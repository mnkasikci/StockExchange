CREATE PROCEDURE [dbo].[spConsumeSellOffer]
@SellofferID int,
@Selleramount int,
@unitamount int 
as
	if(@unitamount > @Selleramount) RAISERROR('Wrong call to the stored procedure, attempt to overuse buyoffer',20,1) with log;

	if(@unitamount = @Selleramount) 
		delete from 
			SellOffers
		where 
			SellOffers.Id = @SellofferID
	
	else
	begin
		DECLARE @finalamount int = @Selleramount - @unitamount
		update SellOffers
		set
			Amount = @finalamount 
		where
			SellOffers.Id = @SellofferID
	end

	
	