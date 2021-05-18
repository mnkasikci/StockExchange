CREATE TRIGGER DeleteItemWhenZero
ON [dbo].UserItems
After update
AS
BEGIN
	SET NOCOUNT ON
	declare @remainingAmount int = (select Amount from inserted)
	
	if(@remainingAmount > 0) return

	delete from
		UserItems
	where
		UserItems.Amount=0


END
