CREATE TABLE [dbo].[SellOffers]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[OffererId] nvarchar(128) NOT NULL FOREIGN KEY REFERENCES Users(Id),
	[ItemTypeId] INT NOT NULL FOREIGN KEY REFERENCES ItemTypes(Id),
	[Amount] INT NOT NULL CHECK (Amount >=0) ,
	[UnitPrice] Decimal(10,2) NOT NULL CHECK (UnitPrice>0),
	[CreateDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE()
)
