CREATE TRIGGER [TriggerTryFindMatchforSellOffer]
	ON [dbo].[Selloffers]
	AFTER INSERT
	AS
	BEGIN
		SET NOCOUNT ON
		DECLARE @Id int = (select Id from inserted)
		declare @returnValue int 

	exec @returnValue = spTryFindMatchAndUpdateTables @id, null

	while(@returnValue =1)
		exec @returnValue = spTryFindMatchAndUpdateTables @id, null

	END
