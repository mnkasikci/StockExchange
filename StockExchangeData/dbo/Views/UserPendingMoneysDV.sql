CREATE VIEW [dbo].[UserPendingMoneysDv]
	AS SELECT
			t1.Id PendingId,
			t2.FirstName,
			t2.LastName,
			t1.Amount,
			t1.CreationDate
		from
			UserPendingMoneys as t1
		inner join 
			Users as t2
		on
			t1.UserId=t2.Id
		where 
			t1.MoneyStatus = 0 -- set enum later
		
			
