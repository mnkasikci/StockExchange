﻿CREATE PROCEDURE [dbo].spGetAllPendingMoneys
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
