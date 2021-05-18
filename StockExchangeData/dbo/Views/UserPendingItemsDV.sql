CREATE VIEW [dbo].[UserPendingItemsDV]
	AS SELECT
			t1.Id PendingId,
			t2.FirstName,
			t2.LastName,
			t3.ItemTypeName,
			t1.Amount,
			t1.CreationDate
		from
			UserPendingItems as t1
		inner join 
			Users as t2
		on
			t1.UserId=t2.Id
		inner join 
			ItemTypes as t3
		on
			t1.ItemTypeId = t3.Id
		where 
			t1.ItemStatus = 0 -- set enum later
		
			
