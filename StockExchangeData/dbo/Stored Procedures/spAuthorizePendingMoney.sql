CREATE PROCEDURE [dbo].[spAuthorizePendingMoney]
@PendingMoneyID int,
@AuthorizerID nvarchar(128),
@CurrencyRate DECIMAL(20,10)
AS
begin
	set nocount on;
	--Add to user Moneys
	declare @UserId nvarchar(128) 
	declare @amount Decimal(10,2)
	select 
		@UserId = ui.UserId,
		@amount = ui.Amount
	from
		UserPendingMoneys as ui 
	where
		@PendingMoneyID = ui.Id
		and ui.MoneyStatus= 0 --use enum for here

	if(@UserId is null) RAISERROR('Couldn''t find a money entry with the Id',20,1) with log;

	DECLARE @CalculatedAmount DECIMAL(20,10) = @CurrencyRate * @amount

	exec spUpsertMoney @UserId, @CalculatedAmount;
	--Set values from pending items
	update UserPendingMoneys
	set
		AuthorizationDate = GETUTCDATE(),
		AuthorizedById = @AuthorizerID,
		MoneyStatus = 1 -- use enum for here
	where
		Id = @PendingMoneyID
		
end