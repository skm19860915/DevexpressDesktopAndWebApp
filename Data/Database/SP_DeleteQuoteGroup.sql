--USE [Beta]
GO
/****** Object:  StoredProcedure [dbo].[DeleteQuoteRequestData]    Script Date: 2/25/2021 7:24:38 AM ******/
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
	delete from QuoteToResultsMappers where QuoteGroupID = @GBID
	delete from [FlightItineraries] where FlightItineraryID = @GBID
	delete from QuoteRequestResorts where QuoteGroupId = @GBID
	delete from Transportations where QuoteGroupId = @GBID
	delete from Leg where QuoteGroupId= @GBID
END