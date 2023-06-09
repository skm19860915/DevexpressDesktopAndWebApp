/****** Object:  StoredProcedure [dbo].[ClearTestData]    Script Date: 11/15/2020 9:52:57 AM ******/
use dev
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
drop procedure [dbo].[ClearTestData];
go
create procedure [dbo].[ClearTestData]
as
Begin
delete from QuoteToResultsMappers;
delete from QuoteRequestResorts;
delete from FilteredAccommodations;
delete from AIFilterMaps;
delete from Filters;
delete from Quotes;
delete from Transportations;
delete from AmenityMaps;
delete from FlightItineraries;
delete from Leg;
delete from QuoteRequests;
delete from Booking;
delete from UserMaps;
delete from Notes;
delete from Tasks
delete from Opportunities;
--delete from AspNetUsers where id <> 'eric@eze2travel.com' and id <> 'Eze2Travel' and id <> 'aiden@eze2travel.com' and id <> 'silke@eze2travel.com'
delete from Leg;
delete from LogMsgs;
delete from Staging_Flights;
delete from Staging_HotelRates;
delete from Staging_Hotels;

End;

exec [ClearTestData];
