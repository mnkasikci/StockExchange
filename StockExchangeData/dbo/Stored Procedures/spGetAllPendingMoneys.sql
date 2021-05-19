CREATE PROCEDURE [dbo].spGetAllPendingMoneys
as
	Select
		PendingId,
		FirstName,
		LastName,
		Amount,
		CreationDate
	from
		UserPendingMoneysDv
