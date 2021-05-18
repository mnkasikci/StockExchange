CREATE PROCEDURE [dbo].spRefusePendingItem
@PendingItemID int,
@RefuserID nvarchar(128)
AS
begin
	set nocount on;
	update UserPendingItems
	set
		AuthorizationDate = GETUTCDATE(),
		AuthorizedById = @RefuserID,
		ItemStatus = 2 -- use enum for here
	where
		Id = @PendingItemID
		
end