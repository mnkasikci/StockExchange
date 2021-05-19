CREATE PROCEDURE [dbo].[spAuthorizePendingMoney]
@PendingMoneyID int,
@AuthorizerID nvarchar(128)
AS
begin
	set nocount on;
	--Add to user Moneys
	declare @UserID nvarchar(128) 
	declare @amount Decimal(10,2)
	select 
		@UserID = ui.UserId,
		@amount = ui.Amount
	from
		UserPendingMoneys as ui 
	where
		@PendingMoneyID = ui.Id
		and ui.MoneyStatus= 0 --use enum for here

	if(@UserID is null) RAISERROR('Couldn''t find a money entry with the ID',10,1);

	exec spUpsertMoney @UserID, @amount;
	--Set values from pending items
	update UserPendingMoneys
	set
		AuthorizationDate = GETUTCDATE(),
		AuthorizedById = @AuthorizerID,
		MoneyStatus = 1 -- use enum for here
	where
		Id = @PendingMoneyID
		
end