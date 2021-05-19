CREATE PROCEDURE [dbo].[spGetUserMoneyByID]
 @UserID nvarchar(128)
AS
begin
	select
		Amount
	from 
		UserMoneys
	WHERE
		UserMoneys.UserId = @UserID
end
