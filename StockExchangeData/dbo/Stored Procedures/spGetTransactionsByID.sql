CREATE PROCEDURE [dbo].spGetTransactionsByID
@UserId nvarchar(128) 
AS
	set nocount on
	select 
		* 
	from CompletedTransactionsDV as ct
	where 
		ct.BuyerId = @UserId or ct.SellerId = @UserId
