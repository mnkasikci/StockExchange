CREATE PROCEDURE [dbo].spGetItemTypes
AS
	set nocount on;
	SELECT
		[Id] as ItemTypeID,
		[ItemTypeName]
	from
		[dbo].[ItemTypes]
