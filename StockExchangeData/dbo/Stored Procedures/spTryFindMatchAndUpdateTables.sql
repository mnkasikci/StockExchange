Create Procedure [dbo].[spTryFindMatchAndUpdateTables](
@SellOfferID int = null,
@BuyOfferID int = null)
as
begin
	set nocount on
	if @SellOfferID is null and @BuyOfferID is null return -1
	declare @ItemTypeID int
	
	declare @SellerID nvarchar(128)
	declare @BuyerID nvarchar(128)

	declare @ValidPrice decimal(10,2)
	declare @SellerPrice decimal(10,2)
	declare @BuyerPrice decimal(10,2)

	declare @selleramount int
	declare @buyeramount int
	declare @minamount int

	declare @transfermoneyamount decimal(10,2)

	declare @BuyerCreateDate datetime2
	declare @SellerCreateDate datetime2

	if @SellOfferID is null -- If the caller wants us to find Sell offer for given buyoffer
	begin
		
		select @buyeramount = Amount, @BuyerPrice = UnitPrice, @Buyerid = t1.UserId, @BuyerCreateDate = t1.CreateDate, @ItemTypeID = t1.ItemTypeID from BuyOffers as t1 where @BuyOfferID = t1.id
		select top 1 
			@SellerCreateDate = t1.CreateDate,
			@SellOfferID = t1.Id,
			@SellerID = t1.UserId,
			@selleramount = t1.Amount,
			@sellerprice = t1.UnitPrice
		from
			SellOffers as t1
		where
			t1.ItemTYpeID = @ItemTypeID and
			t1.UnitPrice <= @BuyerPrice
		order by 
			t1.UnitPrice asc, t1.CreateDate asc
	end

	else   -- If the caller wants us to find buy offer for given selloffer
	begin
		select @selleramount = Amount, @SellerPrice = UnitPrice, @sellerid = UserID, @SellerCreateDate = CreateDate, @itemTypeID = t1.ItemTypeID from SellOffers as t1 where @SellOfferID = t1.id
		select top 1
			@BuyOfferID = t1.Id,
			@BuyerID= t1.UserID,
			@BuyerCreateDate = t1.CreateDate,
			@buyeramount = t1.Amount,
			@BuyerPrice = t1.UnitPrice
		from
			BuyOffers as t1
		where
			t1.ItemTYpeID = @ItemTypeID and
			t1.UnitPrice >= @SellerPrice
		order by 
			t1.UnitPrice desc, t1.CreateDate asc

	end
	if @SellOfferID is null or @BuyOfferID is null return 0 -- if still one of them is null, meaning there is no suitable match
	

	set @ValidPrice = IIF(@BuyerCreateDate < @SellerCreateDate,@buyerprice,@sellerprice)
	set @minamount =  IIF(@buyeramount < @selleramount,@buyeramount,@selleramount) 
	if(@minamount<=0) return 0

	set @transfermoneyamount = @ValidPrice * @minamount

	Exec spUpsertMoney @sellerid,@transfermoneyamount
	Exec spUpsertItem @Buyerid,@ItemTypeID,@minamount
	
	Exec spConsumeBuyOffer @BuyofferID,@buyeramount,@minamount
	Exec spConsumeSellOffer @SellofferID, @selleramount,@minamount

	insert into CompletedTransactions (SellofferCreationDate,BuyofferCreationDate,SellerID, BuyerID, Amount,UnitPrice,ItemTypeID)
	values (@SellerCreateDate,@BuyerCreateDate,@sellerID, @buyerID, @minamount,@ValidPrice,@ItemTypeID)

	return 1
end