CREATE PROCEDURE [dbo].[spCreateBuyOffer]
@OffererID nvarchar(128),
@ItemTypeId int,
@Amount int,
@UnitPrice Decimal(10,2)
AS
	IF (@UnitPrice <=0 OR @Amount <=0)
	RAISERROR('Can''t create buy offer with less or equal to zero amount or unit price',20,1) with log;

	DECLARE @InitialUserMoneyAmount decimal(10,2), @TotalMoneyRequired decimal(10,2), @ComissionFee decimal(10,2), @PurchasePrice decimal(10,2)

	

	set @PurchasePrice = @UnitPrice * @Amount
	set @ComissionFee = @PurchasePrice / 100
	set @TotalMoneyRequired = @PurchasePrice + @ComissionFee

	exec spGetUserMoneyByID @OffererID, @InitialUserMoneyAmount output
	IF @InitialUserMoneyAmount is null or @InitialUserMoneyAmount < @TotalMoneyRequired 
		RAISERROR('Offerer doesn''t have enough money',20,1) with log;

	--Paying comission fee
	declare @comissionerID nvarchar(128)
	exec [spGetComissionerId] @comissionerID output
	exec spUpsertMoney @comissionerId,@comissionFee


	--Decreasing offerer money
	declare @DownsertMoneyAmount decimal(10,2) = - @TotalMoneyRequired
	exec spUpsertMoney @OffererID,@DownsertMoneyAmount

	--creating the offer
	INSERT INTO BuyOffers ([OffererId],ItemTypeId,Amount,UnitPrice,CreateDate)
	VALUES (@OffererID,@ItemTypeId,@Amount,@UnitPrice,GETUTCDATE())


	return


