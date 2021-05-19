CREATE VIEW [dbo].[CompletedTransactionsDV]
	AS 
	SELECT 
		ct.Id TransactionID,
		IIF(ct.BuyofferCreationDate > ct.SellofferCreationDate, ct.BuyofferCreationDate , ct.SellOfferCreationDate) as TransactionDate,
		u.FirstName,
		u.LastName,
		it.ItemTypeName,
		ct.Amount,
		ct.UnitPrice
	FROM 
		CompletedTransactions as ct
	inner join 
		Users as u
	on
		u.Id = ct.BuyerId or 
		u.Id = ct.SellerId
	inner join 
		ItemTypes as it
	on 
		it.Id = ct.ItemTypeID
