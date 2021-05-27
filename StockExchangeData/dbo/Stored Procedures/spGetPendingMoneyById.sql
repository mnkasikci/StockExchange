CREATE PROCEDURE [dbo].spGetPendingMoneyById
@PendingID int
as
	Select
		PendingId,
		FirstName,
		LastName,
		Amount,
		CurrencyCode,
		CurrencyName,
		CreationDate
	from
		UserPendingMoneysDv
	where
		@PendingID = PendingId
