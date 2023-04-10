/****** Object:  StoredProcedure [dbo].[ClearProdData]    Script Date: 11/15/2020 9:54:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[ClearProdData]
as
Begin
delete from QuoteToResultsMappers;
delete from QuoteRequestResorts;
delete from AIFilterMaps;
delete from Filters;
delete from Quotes;
delete from Transportations;
delete from AmenityMaps;
delete from QuoteRequests;
delete from Booking;
delete from Opportunities;
delete from Leg;
delete from LogMsgs;
delete from Staging_Flights;
delete from Staging_HotelRates;
delete from Staging_Hotels;

End;
GO


