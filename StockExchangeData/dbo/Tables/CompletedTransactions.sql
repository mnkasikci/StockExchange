CREATE TABLE [dbo].[CompletedTransactions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	SellerId nvarchar(128) not null foreign key references Users(Id),
	BuyerId nvarchar(128) not null foreign key references Users(Id),
	ItemTypeID int not null foreign key references Itemtypes(Id),
	SellofferCreationDate DATETIME2 NOT NULL,
	BuyofferCreationDate DATETIME2 NOT NULL,
	[Amount] INT NOT NULL,
	[UnitPrice] DECIMAL(10,2) NOT NULL,

)
