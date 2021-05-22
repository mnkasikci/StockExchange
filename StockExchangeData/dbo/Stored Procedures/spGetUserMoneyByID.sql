CREATE PROCEDURE [dbo].[spGetUserMoneyByID]
 @UserId nvarchar(128)
AS
begin
	declare @money decimal
	select
		@money = Amount
	from 
		UserMoneys
	WHERE
		UserMoneys.UserId = @UserId

	select IIF(@money is null,0,@money)
end
