CREATE PROCEDURE [dbo].[spGetUserItemsByID]
 @UserID nvarchar(128)
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
end
