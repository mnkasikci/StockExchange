CREATE PROCEDURE [dbo].[spGetComissionerId]
@ComissionerId nvarchar(128) output
AS
	Select 
		@ComissionerId = Id
	From 
		Users
	where
		TCIDNumber = '11111111111'
	
RETURN 0
