CREATE FUNCTION [dbo].[funcCalculatePrice]
(
	@itemtypeid int,
	@itemamount int
)
RETURNS decimal(10,2)
AS
BEGIN
	
	DECLARE @rowamount int, @totalamount int=0, @unitprice decimal(10,2), @rowcost decimal(10,2), @totalcost decimal(10,2) =0
	DECLARE myCursor CURSOR FORWARD_ONLY FOR
	select 
		s1.Amount as Amount,
		s1.Unitprice as Unitprice
	from
		SellOffers as s1
	where
		s1.ItemTypeID=@itemtypeid 
	order by
		s1.UnitPrice,s1.CreateDate
	OPEN myCursor;

	WHILE 1=1
	BEGIN
		FETCH NEXT FROM myCursor INTO @rowamount,@unitprice
		if(@@FETCH_STATUS<>0) break
		if(@totalamount + @rowamount > @Itemamount) set @rowamount = @ItemAmount - @totalamount
		set @rowcost = @unitprice * @rowamount
		set @totalcost += @rowcost
		set @totalamount += @rowamount

		if(@totalamount = @itemamount) break

	END

	CLOSE myCursor;
	DEALLOCATE myCursor;

	if(@totalamount = @itemamount) return @totalcost
	
	
	return -1 -- as indicator that there isn't enough item on the market
	
END
