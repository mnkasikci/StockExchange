CREATE PROCEDURE [dbo].spGetItemTypes
AS
	set nocount on;
	SELECT
		[Id],
		[ItemTypeName]
	from
		[dbo].[ItemTypes]
