CREATE PROCEDURE [dbo].[spConsumeBuyOffer]
@BuyofferID int,
@Buyeramount int,
@Transferamount int 
as
	if(@Transferamount > @Buyeramount) RAISERROR('Wrong call to the stored procedure, attempt to overuse buyoffer',20,1) with log;

	if(@Transferamount = @Buyeramount) 
		delete from 
			BuyOffers
		where 
			BuyOffers.ID = @BuyofferID
	
	else
	begin
		DECLARE @finalamount int = @Buyeramount - @Transferamount
		update BuyOffers
		set
			Amount = @finalamount 
		where
			BuyOffers.ID = @BuyofferID
	end

	
	