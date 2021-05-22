CREATE VIEW [dbo].[CompletedTransactionsDV]
	AS 
	SELECT 
		ct.Id TransactionId,
		IIF(ct.BuyOfferCreationDate > ct.SellOfferCreationDate, ct.BuyOfferCreationDate , ct.SellOfferCreationDate) as TransactionDate,
		u.Id BuyerId,
		u.FirstName BuyerFirstName,
		u.LastName BUyerLastName,
		usellers.Id SellerId,
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
