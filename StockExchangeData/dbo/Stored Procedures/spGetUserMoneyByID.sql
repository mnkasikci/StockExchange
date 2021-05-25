CREATE PROCEDURE [dbo].[spGetUserMoneyByID](
 @UserId nvarchar(128),
 @Usermoney decimal(10,2) output)
AS
begin
	declare @money decimal(10,2);
	select
		@money = Amount
	from 
		UserMoneys
	WHERE
		UserId = @UserId
	set @Usermoney = IIF(@money is null,0,@money)
end
