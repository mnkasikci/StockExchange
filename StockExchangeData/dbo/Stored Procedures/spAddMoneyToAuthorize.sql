CREATE PROCEDURE [dbo].[spAddMoneyToAuthorize]
@UserID nvarchar(128),
@Amount DECIMAL(10,2)
as
	IF (@Amount <=0)
		RAISERROR('Money amount should be bigger than 0',20,1) with log;
	


	declare @PendingStatusIndicator int
	set @PendingStatusIndicator = 0 -- Review it later, use enum ? 
		INSERT into UserPendingMoneys(UserId,Amount,CreationDate,MoneyStatus)
		values (@UserID,@Amount,getutcdate(),@PendingStatusIndicator)

		
RETURN 0
