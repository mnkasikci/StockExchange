CREATE TABLE [dbo].[UserPendingMoneys]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[UserId] nvarchar(128) NOT NULL FOREIGN KEY REFERENCES Users(Id),
	[Amount] DECIMAL(10,2) NOT NULL DEFAULT (0),
	[CurrencyCode] nvarchar(10) NOT NULL FOREIGN KEY REFERENCES CurrencyTypes(CurrencyCode),
	[CreationDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[AuthorizationDate] DATETIME2,
	[AuthorizedById] nvarchar(128) FOREIGN KEY REFERENCES Users(Id),
	MoneyStatus INT NOT NULL DEFAULT (0)
)
