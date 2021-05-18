CREATE PROCEDURE [dbo].[spCreateSellOffer]
@ItemIndexID int,
@Amount int,
@UnitPrice Decimal(10,2)
AS
	IF (@UnitPrice <=0 OR @Amount <=0)
	RAISERROR('Can''t create sell offer with less or equal to zero amount or unit price',10,1);
	DECLARE @UserId nvarchar(128), @ItemTypeId int, @InitialAmount int

	SELECT 
		@InitialAmount = Amount,
		@UserId = UserId,
		@ItemTypeId = ItemTypeId
	From
		UserItems 
	WHERE
		@ItemIndexID = Id

	IF @InitialAmount is null or @InitialAmount < @Amount 
	RAISERROR('Creator don''t have enough items',10,1);

	--delete or update the user items table
	IF @InitialAmount = @Amount
		DELETE FROM UserItems WHERE @ItemIndexID = Id
	ELSE
	BEGIN
		UPDATE UserItems
		SET
			Amount = Amount - @Amount
		WHERE
			@ItemIndexID = Id
	END
	--create the offer
	INSERT INTO SellOffers (UserId,ItemTypeID,Amount,UnitPrice,CreateDate)
	VALUES (@UserId,@ItemTypeId,@Amount,@UnitPrice,GETUTCDATE())


	return


