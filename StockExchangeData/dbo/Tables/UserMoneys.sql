CREATE TABLE [dbo].UserMoneys
(
	[Id] int NOT NULL PRIMARY KEY Identity(1,1),
	[UserId] nvarchar(128) not null foreign key references Users(Id),
	Amount DECIMAL (10,2) not null


)
