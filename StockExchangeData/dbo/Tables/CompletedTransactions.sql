CREATE TABLE [dbo].[CompletedTransactions]
(
	[Id] INT NOT NULL PRIMARY KEY IdENTITY(1,1),
	SellerId nvarchar(128) not null foreign key references Users(Id),
	BuyerId nvarchar(128) not null foreign key references Users(Id),
	ItemTypeId int not null foreign key references ItemTypes(Id),
	SellOfferCreationDate DATETIME2 NOT NULL,
	BuyOfferCreationDate DATETIME2 NOT NULL,
	[Amount] INT NOT NULL,
	[UnitPrice] DECIMAL(10,2) NOT NULL,

)
