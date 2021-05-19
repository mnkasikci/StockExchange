CREATE PROCEDURE [dbo].[spCreateSellOffer]
@OffererID nvarchar(128),
@ItemTypeID int,
@Amount int,
@UnitPrice Decimal(10,2)
AS
	IF (@UnitPrice <=0 OR @Amount <=0)
	RAISERROR('Can''t create sell offer with less or equal to zero amount or unit price',10,1);
	DECLARE @InitialItemAmount int, @ItemIndexID int

	SELECT 
		@InitialItemAmount = Amount,
		@ItemIndexID = Useritems.Id
	From
		UserItems 
	WHERE
		@ItemTypeID = UserItems.ItemTypeId and
		@OffererID = UserItems.UserId

	IF @InitialItemAmount is null or @InitialItemAmount < @Amount 
	RAISERROR('Creator don''t have enough items',10,1);

	--delete or update the user items table
	IF @InitialItemAmount = @Amount
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
	VALUES (@OffererID,@ItemTypeId,@Amount,@UnitPrice,GETUTCDATE())


	return


