CREATE PROCEDURE [dbo].[spTryMarketOrder]
@UserId nvarchar(128),
@ItemTypeId int,
@ItemAmount int,
@totalPrice decimal(10,2)
AS
	declare @calculatedAmount decimal(10,2)
	set @calculatedAmount = [dbo].[funcCalculatePrice](@itemtypeid,@itemamount)

	if(@calculatedAmount =-1)
		RAISERROR('Not enough item left on the market',20,1) with log ;
	if @calculatedAmount<> @totalprice 
		RAISERROR('Item price changed, please recalculate and recreate the order.',20,1) with log ;
		
	
	declare @usermoney decimal(10,2)
	exec spGetUserMoneyByID @userid, @usermoney output
	if(@usermoney < @calculatedAmount)
		RAISERROR('User doesn''t have enough money',20,1) with log ;

	--decrease the money from the user
	declare @decreaseMoneyAmount decimal(10,2) = 0-@calculatedAmount
	exec spUpsertMoney @userid,@decreaseMoneyAmount


	--create temp table 
	select 
		s1.CreateDate as SellOfferCreationDate,
		s1.[OffererId] as SellerId,
		s1.Id as SellOfferId,
		s1.Amount as Amount,
		s1.Unitprice as Unitprice,
		s2.PrevItemTotal as PrevItemTotal
	into
		#temptable
	from
		SellOffers as s1
	inner join 
		(select isnull((sum (Amount) over (partition by ItemTypeId order by unitprice,createDate ROWS between unbounded Preceding and 1 Preceding)),0) as PrevItemTotal, Id from SellOffers) as s2
	on 
		s1.Id = s2.id
	where
		s1.ItemTypeID=@itemtypeid and
		s2.PrevItemTotal < @itemamount
	order by
		unitprice,createDate
		--------------
	--create cursor and make transactions
	declare
	@sellerid nvarchar(128),
	@buyerID nvarchar(128) = @userid,
	@sellofferId int,
	@buyofferId int = null,
	@unitprice decimal(10,2),
	@buyeramount int,
	@selleramount int,
	@Unitamount int,
	@transfermoneyamount decimal(10,2),
	@BuyerCreateDate datetime2 = getutcdate(),
	@SellerCreateDate datetime2,
	@prevtotal int

	DECLARE myCursor CURSOR FORWARD_ONLY FOR
	SELECT 	
		SellOfferCreationDate,
		SellerId,
		SellOfferId,
		Amount,
		Unitprice,
		PrevItemTotal
	FROM #temptable	;
	OPEN myCursor;
	

	WHILE 1=1
	BEGIN
		FETCH NEXT FROM myCursor INTO @SellerCreateDate,@sellerid,@sellofferId,@selleramount,@unitprice,@prevtotal
		if(@@FETCH_STATUS<>0) break
		set @buyeramount = IIF(@prevtotal + @selleramount > @Itemamount, @ItemAmount - @prevtotal,@selleramount)
		
		set @Unitamount = @buyeramount
		set @transfermoneyamount = @Unitamount * @unitprice

		Exec spMakeTransaction @sellerid,@buyerid,@sellofferid,@buyofferId,@itemTypeId,@unitPrice,@buyerAmount,@selleramount,@unitamount,@transfermoneyamount,@Buyercreatedate,@sellercreatedate

	END
	CLOSE myCursor;
	DEALLOCATE myCursor;

