CREATE TABLE [dbo].[ItemTypes]
(
	[Id] INT NOT NULL PRIMARY KEY IdENTITY(1,1),
	[ItemTypeName] NVARCHAR(50) NOT NULL UNIQUE,
)
