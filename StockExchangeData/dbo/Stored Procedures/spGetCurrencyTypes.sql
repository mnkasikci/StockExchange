CREATE PROCEDURE [dbo].[spGetCurrencyTypes]
AS
	SELECT
		CurrencyCode,
		CurrencyName
	FROM
		CurrencyTypes
