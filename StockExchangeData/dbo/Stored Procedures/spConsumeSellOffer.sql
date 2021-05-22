CREATE PROCEDURE [dbo].[spConsumeSellOffer]
@SellofferID int,
@Selleramount int,
@Transferamount int 
as
	if(@Transferamount > @Selleramount) RAISERROR('Wrong call to the stored procedure, attempt to overuse buyoffer',20,1) with log;

	if(@Transferamount = @Selleramount) 
		delete from 
			SellOffers
		where 
			SellOffers.ID = @SellofferID
	
	else
	begin
		DECLARE @finalamount int = @Selleramount - @Transferamount
		update SellOffers
		set
			Amount = @finalamount 
		where
			SellOffers.Id = @SellofferID
	end

	
	