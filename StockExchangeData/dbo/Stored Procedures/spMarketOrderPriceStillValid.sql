CREATE PROCEDURE [dbo].[spMarketOrderPriceStillValid]
@UserId nvarchar(128),
@ItemTypeId int,
@ItemAmount int,
@totalPrice decimal(10,2)
AS
	declare @calculatedPrice decimal(10,2)
	set @calculatedPrice = [dbo].[funcCalculatePrice](@itemtypeid,@itemamount)

	if(@calculatedPrice = @totalPrice) select 1;
	else select 0;
