CREATE TRIGGER [TriggerTryFindMatchforBuyOffer]
	ON [dbo].[BuyOffers]
	AFTER INSERT
	AS
	BEGIN
		SET NOCOUNT ON
		DECLARE @Id int = (select Id from Inserted)
		declare @returnValue int 

	exec @returnValue = spTryFindMatchAndUpdateTables NULL, @id

	while(@returnValue =1)
		exec @returnValue = spTryFindMatchAndUpdateTables NULL, @Id

	END
