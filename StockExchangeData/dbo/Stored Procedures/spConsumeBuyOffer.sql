CREATE PROCEDURE [dbo].[spConsumeBuyOffer]
@BuyofferId int,
@Buyeramount int,
@unitamount int 
as
	
	if(@unitamount > @Buyeramount) RAISERROR('Wrong call to the stored procedure, attempt to overuse buyoffer',20,1) with log;

	if(@unitamount = @Buyeramount) 
		delete from 
			BuyOffers
		where 
			BuyOffers.Id = @BuyofferId
	
	else
	begin
		DECLARE @finalamount int = @Buyeramount - @unitamount
		update BuyOffers
		set
			Amount = @finalamount 
		where
			BuyOffers.Id = @BuyofferId
	end

	
	