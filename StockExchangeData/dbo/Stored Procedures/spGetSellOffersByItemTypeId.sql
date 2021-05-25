CREATE PROCEDURE [dbo].[spGetSellOffersByItemTypeId]
	@ItemTypeId INT
AS
BEGIN
	SET NOCOUNT ON
	SELECT 
		*
	FROM 
		SellOffersDV
	WHERE
		ItemTypeId = @ItemTypeId
END