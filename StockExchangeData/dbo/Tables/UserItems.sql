CREATE TABLE [dbo].UserItems
(
	[Id] int NOT NULL PRIMARY KEY Identity(1,1),
	[UserId] nvarchar(128) not null foreign key references Users(Id),
	[ItemTypeId] int not null foreign key references ItemTypes(Id),
	Amount int not null default(0)


)
