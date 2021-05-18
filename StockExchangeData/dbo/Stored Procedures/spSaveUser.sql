CREATE PROCEDURE [dbo].[spSaveUser]
	@Id nvarchar(128)
	,@FirstName nvarchar(50)
	,@LastName nvarchar(50)
	,@TCIDNumber nvarchar(11)
	,@PhoneNumber nvarchar(10)
	,@Address nvarchar(max)
AS
	begin 
		set nocount on;
		insert into [dbo].[Users]
		([Id],[FirstName],[LastName],[TCIDNumber],[PhoneNumber],[Address])
		values
		(@Id,@FirstName,@LastName,@TCIDNumber,@PhoneNumber,@Address)
	end
RETURN 0
/*        public string ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string TCIDNumber { get; set; }
		public string PhoneNumber { get; set; }
		public string Address { get; set; } */

/*	Id NVARCHAR(128) NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(50) NOT NULL, 
	[TCIDNumber] VARCHAR(11) NOT NULL UNIQUE, 
	[PhoneNumber] VARCHAR(10) NOT NULL UNIQUE, 
	[Address] NVARCHAR(MAX) NOT NULL,
*/