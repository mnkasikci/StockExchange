CREATE PROCEDURE [dbo].[spGetAllTransactions]
AS
	set nocount on
	select * from CompletedTransactionsDV
