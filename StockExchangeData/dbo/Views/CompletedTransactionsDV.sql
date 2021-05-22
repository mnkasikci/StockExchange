CREATE VIEW [dbo].[CompletedTransactionsDV]
	AS 
	SELECT 
		ct.Id TransactionID,
		IIF(ct.BuyofferCreationDate > ct.SellofferCreationDate, ct.BuyofferCreationDate , ct.SellOfferCreationDate) as TransactionDate,
		u.ID BuyerID,
		u.FirstName BuyerFirstName,
		u.LastName BUyerLastName,
		usellers.ID SellerId,
		usellers.FirstName SellerFirstname,
		usellers.LastName SellerLastName,
		it.ItemTypeName,
		ct.Amount,
		ct.UnitPrice
	FROM 
		CompletedTransactions as ct
	inner join 
		Users as u
	on
		u.Id = ct.BuyerId
	inner join
		Users as usellers
	on
		usellers.Id = ct.SellerId
	inner join 
		ItemTypes as it
	on 
		it.Id = ct.ItemTypeId
