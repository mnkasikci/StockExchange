CREATE PROCEDURE [dbo].spGetTransactionsByID
@UserId nvarchar(128) 
AS
	set nocount on
	select 
		* 
	from CompletedTransactionsDV as ct
	where 
		ct.BuyerID = @UserId or ct.SellerId = @UserID
