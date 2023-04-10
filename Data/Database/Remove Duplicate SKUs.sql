USE [Dev]

Declare @SKUID as int

Declare Enum Cursor Local Static Read_Only FORWARD_ONLY for
	select SKUID from SKUs 
		where SKUID not in 
		(
			Select Min(SKuID) as MaxSKUID
			from SKUs
			Group BY Name, ProviderID
		)			

Open Enum Fetch Next from Enum into @SKUID While @@FETCH_STATUS = 0
Begin
	--Print @SKUID
	delete from SKUs where SKUID = @SKUID
	FETCH NEXT FROM Enum INTO @SKUID
End
Close Enum
DEALLOCATE  Enum

