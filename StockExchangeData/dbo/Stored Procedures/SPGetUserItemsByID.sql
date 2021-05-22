CREATE PROCEDURE [dbo].[spGetUserItemsByID]
 @UserId nvarchar(128)
AS
begin
	select
		ItemId,
		ItemTypeId,
		[UserId],
		FirstName,
		LastName,
		ItemTypeName,
		Amount
	from 
		UserItemsDetailedView
	where
		[UserId] = @UserId
end
