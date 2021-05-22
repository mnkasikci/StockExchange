CREATE PROCEDURE [dbo].spRefusePendingMoney
@PendingMoneyID int,
@RefUserId nvarchar(128)
AS
begin
	set nocount on;
	update UserPendingMoneys
	set
		AuthorizationDate = GETUTCDATE(),
		AuthorizedById = @RefUserId,
		MoneyStatus = 2 -- use enum for here
	where
		Id = @PendingMoneyID
		
end