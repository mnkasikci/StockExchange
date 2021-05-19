CREATE TRIGGER [TriggerTryFindMatchforBuyOffer]
	ON [dbo].[Buyoffers]
	AFTER INSERT
	AS
	BEGIN
		SET NOCOUNT ON
		DECLARE @Id int = (select Id from inserted)
		declare @returnValue int 

	exec @returnValue = spTryFindMatchAndUpdateTables NULL, @id

	while(@returnValue =1)
		exec @returnValue = spTryFindMatchAndUpdateTables NULL, @ID

	END
