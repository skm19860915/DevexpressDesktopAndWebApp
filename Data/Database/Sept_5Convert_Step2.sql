
ALTER PROCEDURE [dbo].[DeleteQuoteRequestData]
(
    @aQRId int 
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

	delete from Staging_HotelRates where HotelStagingID in ( select HotelStagingID from staging_hotels where RequestID = @aQRId );
	delete from Staging_Hotels where RequestID = @aQRId;
	select count(*) from Staging_HotelRates;
END

Create PROCEDURE [dbo].[DeleteQuoteGroup]
(
	@GBID as int
)
AS
BEGIN
--set @GBID = 1213
	delete from QuoteToResultsMappers where QuoteGroupID = @GBID
	delete from QuoteRequestTickets where QuoteGroupId = @GBID
	delete from QuoteRequestResorts where QuoteGroupId = @GBID
	delete from Transportations where QuoteGroupId = @GBID
	delete from Leg where QuoteGroupId= @GBID
--select * from QuoteGroups
END