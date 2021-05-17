CREATE PROCEDURE [dbo].[spGetUserDataByID]
	@Id nvarchar(128)
	
AS
	set nocount on
	SELECT
		*
	from
		Users
	where
		@Id = Id

