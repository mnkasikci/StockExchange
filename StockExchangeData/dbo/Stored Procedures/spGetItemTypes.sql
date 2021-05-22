CREATE PROCEDURE [dbo].spGetItemTypes
AS
	set nocount on;
	SELECT
		[Id] as ItemTypeId,
		[ItemTypeName]
	from
		[dbo].[ItemTypes]
