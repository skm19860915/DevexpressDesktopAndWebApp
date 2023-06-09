GO
/****** Object:  StoredProcedure [dbo].[DeleteQuoteGroup]    Script Date: 9/14/2021 1:46:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[DeleteQuoteGroup]
(
	@GBID as int
)
AS
BEGIN
--set @GBID = 1213
	delete from Staging_HotelRates where HotelStagingID in ( 
		select HotelStagingID from Staging_Hotels where QuoteGroupId =  @GBID )

	delete from Staging_Hotels where QuoteGroupId =  @GBID
	delete from Staging_Flights where QuoteGroupId =  @GBID
	delete from QuoteToResultsMappers where QuoteGroupID = @GBID
	delete from [FlightItineraries] where FlightItineraryID = @GBID
	delete from QuoteRequestResorts where QuoteGroupId = @GBID
	delete from Transportations where QuoteGroupId = @GBID
	delete from FlightItineraries where QuoteGroupId = @GBID
	delete from Leg where QuoteGroupId= @GBID
END