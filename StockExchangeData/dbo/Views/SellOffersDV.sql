CREATE VIEW [dbo].SellOffersDV
	AS

	SELECT 
		SO.*,
		IT.ItemTypeName

	FROM
		SellOffers as SO
	INNER JOIN 
		ItemTypes as IT
	ON
		SO.ItemTypeId = IT.Id

