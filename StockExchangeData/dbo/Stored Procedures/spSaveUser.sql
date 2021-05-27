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
