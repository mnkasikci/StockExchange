CREATE PROCEDURE [dbo].[spGetUserMoneyByID]
 @UserID nvarchar(128)
AS
begin
	declare @money decimal
	select
		@money = Amount
	from 
		UserMoneys
	WHERE
		UserMoneys.UserId = @UserID

	select IIF(@money is null,0,@money)
end
