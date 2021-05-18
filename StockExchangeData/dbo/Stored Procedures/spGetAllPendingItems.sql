CREATE PROCEDURE [dbo].spGetAllPendingItems
as
	Select
		PendingId,
		FirstName,
		LastName,
		ItemTypeName,
		Amount,
		CreationDate
	from
		UserPendingItemsDV
