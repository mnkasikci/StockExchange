CREATE PROCEDURE [dbo].[spGetUserDataByTC]
	@TC varchar(11)
	
AS
	set nocount on
	SELECT
		*
	from
		Users
	where
		@TC = TCIDNumber

