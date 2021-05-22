CREATE PROCEDURE [dbo].[spAuthorizePendingItem]
@PendingItemID int,
@AuthorizerID nvarchar(128)
AS
begin
	set nocount on;
	--Add to user items
	declare @UserId nvarchar(128) 
	declare @ItemTypeId int
	declare @amount int
	select 
		@UserId = ui.UserId,
		@ItemTypeId = ui.ItemTypeId,
		@amount = ui.Amount
	from
		UserPendingItems as ui 
	where
		@PendingItemID = ui.Id
		and ui.ItemStatus = 0 --use enum for here

	if(@UserId is null) 	if(@UserId is null) RAISERROR('Couldn''t find an item entry with the Id',10,1);

	exec spUpsertItem @UserId, @ItemTypeId,@amount;
	--Set values from pending items
	update UserPendingItems
	set
		AuthorizationDate = GETUTCDATE(),
		AuthorizedById = @AuthorizerID,
		ItemStatus = 1 -- use enum for here
	where
		Id = @PendingItemID
		
end