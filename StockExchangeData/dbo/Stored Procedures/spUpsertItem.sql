CREATE PROCEDURE [dbo].[spUpsertItem]
@UserId nvarchar(128),
@ItemTypeId int,
@Amount int
AS
	MERGE INTO UserItems AS TARGET
	USING
	(
		SELECT 
			@UserId [ID],
			@ItemTypeId [ItemTypeId],
			@Amount [AMOUNT]
	) AS SOURCE
	ON SOURCE.ID = TARGET.UserId and SOURCE.ItemTypeId = TARGET.ItemTypeId
	WHEN MATCHED
	THEN
		UPDATE SET
		TARGET.Amount = TARGET.Amount + SOURCE.AMOUNT
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (UserId,ItemTypeId,Amount)
	VALUES (SOURCE.ID,SOURCE.ItemTypeId,SOURCE.AMOUNT);
