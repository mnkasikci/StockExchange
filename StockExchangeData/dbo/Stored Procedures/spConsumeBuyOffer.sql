CREATE PROCEDURE [dbo].[spConsumeBuyOffer]
@BuyofferId int,
@Buyeramount int,
@Transferamount int 
as
	if(@Transferamount > @Buyeramount) RAISERROR('Wrong call to the stored procedure, attempt to overuse buyoffer',20,1) with log;

	if(@Transferamount = @Buyeramount) 
		delete from 
			BuyOffers
		where 
			BuyOffers.Id = @BuyofferId
	
	else
	begin
		DECLARE @finalamount int = @Buyeramount - @Transferamount
		update BuyOffers
		set
			Amount = @finalamount 
		where
			BuyOffers.Id = @BuyofferId
	end

	
	