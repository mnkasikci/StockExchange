CREATE VIEW [dbo].[UserPendingMoneysDv]
	AS SELECT
			t1.Id PendingId,
			t2.FirstName,
			t2.LastName,
			t1.Amount,
			ct.CurrencyCode,
			ct.CurrencyName,
			t1.CreationDate
		from
			UserPendingMoneys as t1
		inner join 
			Users as t2
		
		on
			t1.UserId=t2.Id
		inner join
			CurrencyTypes as ct
		on
			t1.CurrencyCode = ct.CurrencyCode
		where 
			t1.MoneyStatus = 0 -- set enum later
		
			
