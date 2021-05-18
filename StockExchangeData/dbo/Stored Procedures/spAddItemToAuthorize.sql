CREATE PROCEDURE [dbo].[spAddItemToAuthorize]
@UserID nvarchar(128),
@ItemTypeID int,
@Amount INT
as
	declare @PendingStatusIndicator int
	set @PendingStatusIndicator = 0 -- Review it later, use enum ? 
		INSERT into UserPendingItems(UserId,ItemTypeId,Amount,CreationDate,ItemStatus)
		values (@UserID,@ItemTypeId,@Amount,getdate(),@PendingStatusIndicator)

		
RETURN 0
