CREATE PROCEDURE [dbo].[spAuthorizePendingItem]
@PendingItemID int,
@AuthorizerID nvarchar(128)
AS
begin
	set nocount on;
	--Add to user items
	declare @UserID nvarchar(128) 
	declare @ItemTypeID int
	declare @amount int
	select 
		@UserID = ui.UserId,
		@ItemTypeID = ui.ItemTypeId,
		@amount = ui.Amount
	from
		UserPendingItems as ui 
	where
		@PendingItemID = ui.Id
		and ui.ItemStatus = 0 --use enum for here

	if(@UserID is null) return

	exec spUpsertItem @UserID, @ItemTypeID,@amount;
	--Set values from pending items
	update UserPendingItems
	set
		AuthorizationDate = GETUTCDATE(),
		AuthorizedById = @AuthorizerID,
		ItemStatus = 1 -- use enum for here
	where
		Id = @PendingItemID
		
end