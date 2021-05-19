CREATE PROCEDURE [dbo].[spAddNewItemType]
@ItemTypeName nvarchar(50)
as
	Insert Into ItemTypes (ItemTypeName)
	values (@ItemTypeName)

		
RETURN 0
