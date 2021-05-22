CREATE PROCEDURE [dbo].[spCreateBuyOffer]
@OffererID nvarchar(128),
@ItemTypeId int,
@Amount int,
@UnitPrice Decimal(10,2)
AS
	IF (@UnitPrice <=0 OR @Amount <=0)
	RAISERROR('Can''t create buy offer with less or equal to zero amount or unit price',20,1) with log;

	DECLARE @InitialUserMoneyAmount decimal(10,2), @TotalMoneyRequired decimal(10,2)

	set @TotalMoneyRequired = @UnitPrice * @Amount


	SELECT 
		@InitialUserMoneyAmount = Amount
	From
		UserMoneys 
	WHERE
		@OffererID = UserMoneys.UserId

	IF @InitialUserMoneyAmount is null or @InitialUserMoneyAmount < @TotalMoneyRequired 
	RAISERROR('Offerer doesn''t have enough money',10,1);
	
	UPDATE UserMoneys
	SET
		Amount = Amount - @TotalMoneyRequired
	WHERE
		UserId = @OffererID

	--create the offer
	INSERT INTO BuyOffers (UserId,ItemTypeId,Amount,UnitPrice,CreateDate)
	VALUES (@OffererID,@ItemTypeId,@Amount,@UnitPrice,GETUTCDATE())


	return


