CREATE PROCEDURE [dbo].[spAddItemToAuthorize]
@UserID nvarchar(128),
@ItemTypeID int,
@Amount INT
as
	IF (@Amount <=0)
		RAISERROR('Item amount should be bigger than 0',20,1) with log ;

	IF NOT EXISTS (SELECT * FROM Itemtypes where ID = @ItemTypeID)
		RAISERROR('Item typeid couldn''t be found',20,1) with log ;

	declare @PendingStatusIndicator int
	set @PendingStatusIndicator = 0 -- Review it later, use enum ? 
		INSERT into UserPendingItems(UserId,ItemTypeId,Amount,CreationDate,ItemStatus)
		values (@UserID,@ItemTypeID,@Amount,getutcdate(),@PendingStatusIndicator)

		
RETURN 0
