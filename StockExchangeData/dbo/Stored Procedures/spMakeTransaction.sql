CREATE PROCEDURE [dbo].[spMakeTransaction]
@SellerId nvarchar(128),
@BuyerId nvarchar(128),
@SellofferId int,
@BuyOfferId int = null,
@ItemTypeId int,
@UnitPrice decimal(10,2),
@BuyerAmount int,
@sellerAmount int,
@Unitamount int,
@TransferMoneyAmount decimal(10,2),
@BuyerCreateDate datetime2,
@SellerCreateDate datetime2
AS
	Exec spUpsertMoney @sellerid,@TransferMoneyAmount
	Exec spUpsertItem @Buyerid,@ItemTypeId,@Unitamount

	if(@BuyOfferId is not null) 
		Exec spConsumeBuyOffer @BuyofferID,@BuyerAmount,@Unitamount
	Exec spConsumeSellOffer @SellofferID, @sellerAmount,@Unitamount

	insert into CompletedTransactions (SellOfferCreationDate,BuyOfferCreationDate,SellerId, BuyerId, Amount,UnitPrice,ItemTypeId)
	values (@SellerCreateDate,@BuyerCreateDate,@SellerId, @BuyerId, @Unitamount,@UnitPrice,@ItemTypeId)
RETURN 0
