Create Procedure [dbo].[spTryFindMatchAndUpdateTables](
@SellOfferID int = null,
@BuyOfferID int = null)
as
begin
	set nocount on
	if @SellOfferID is null and @BuyOfferID is null return -1
	declare @ItemTypeId int
	
	declare @SellerId nvarchar(128)
	declare @BuyerId nvarchar(128)

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
		
		select @buyeramount = Amount, @BuyerPrice = UnitPrice, @BuyerId = t1.[OffererId], @BuyerCreateDate = t1.CreateDate, @ItemTypeId = t1.ItemTypeId from BuyOffers as t1 where @BuyOfferID = t1.Id
		select top 1 
			@SellerCreateDate = t1.CreateDate,
			@SellOfferID = t1.Id,
			@SellerId = t1.[OffererId],
			@selleramount = t1.Amount,
			@sellerprice = t1.UnitPrice
		from
			SellOffers as t1
		where
			t1.ItemTypeId = @ItemTypeId and
			t1.UnitPrice <= @BuyerPrice
		order by 
			t1.UnitPrice asc, t1.CreateDate asc
	end

	else   -- If the caller wants us to find buy offer for given selloffer
	begin
		select @selleramount = Amount, @SellerPrice = UnitPrice, @sellerId = [OffererId], @SellerCreateDate = CreateDate, @ItemTypeId = t1.ItemTypeId from SellOffers as t1 where @SellOfferID = t1.Id
		select top 1
			@BuyOfferID = t1.Id,
			@BuyerId= t1.[OffererId],
			@BuyerCreateDate = t1.CreateDate,
			@buyeramount = t1.Amount,
			@BuyerPrice = t1.UnitPrice
		from
			BuyOffers as t1
		where
			t1.ItemTypeId = @ItemTypeId and
			t1.UnitPrice >= @SellerPrice
		order by 
			t1.UnitPrice desc, t1.CreateDate asc

	end
	if @SellOfferID is null or @BuyOfferID is null return 0 -- if still one of them is null, meaning there is no suitable match
	

	set @ValidPrice = IIF(@BuyerCreateDate < @SellerCreateDate,@buyerprice,@sellerprice)
	set @minamount =  IIF(@buyeramount < @selleramount,@buyeramount,@selleramount) 
	if(@minamount<=0) return 0

	if @BuyerPrice > @ValidPrice  -- refund the extra money to the buyer from the commissioner, since they found a cheaper price than their offer
	begin
		declare @PurchaseRefundAmount decimal(10,2) = (@BuyerPrice - @ValidPrice) * @minAmount
		declare @ComissionFeeRefundAmount decimal (10,2) = @PurchaseRefundAmount / 100
		declare @TotalRefundAmount decimal (10,2) = @PurchaseRefundAmount + @ComissionFeeRefundAmount
		Exec spUpsertMoney @BuyerID,@TotalRefundAmount

		declare @comissionerId nvarchar(128)
		exec spGetComissionerId @comissionerID output
		declare @negativeComissionfee decimal(10,2) = -@ComissionFeeRefundAmount
		Exec spUpsertMoney @comissionerId, @negativeComissionfee
	end

	set @transfermoneyamount = @ValidPrice * @minamount

	
	exec spMakeTransaction @sellerid,@buyerid,@sellofferid,@buyofferId,@itemTypeId,@validprice,@buyeramount,@selleramount,@minamount,@transfermoneyamount,@BuyerCreateDate,@SellerCreateDate

	

	return 1
end
