CREATE VIEW [dbo].UserItemsDetailedView
	AS SELECT
	ui.Id ItemId,
	it.Id ItemTypeId,
	u.Id [UserId],
	u.FirstName,
	u.LastName,
	it.ItemTypeName,
	ui.Amount
	from
	UserItems as ui
	inner join
	Users as u
	on ui.UserId = u.Id
	inner join
	ItemTypes as it
	on ui.ItemTypeId = it.Id
