USE [Blitzer]
GO
/****** Object:  StoredProcedure [dbo].[CleanQA]    Script Date: 10/1/2021 6:29:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[CleanQA]
AS
BEGIN
	declare @GBID as int
	declare @QRID as int
	declare @OPID as int

	Declare Enum Cursor Local Static Read_Only FORWARD_ONLY for
		Select Distinct Id from QuoteGroups

	Open Enum Fetch Next from Enum into @GBID While @@FETCH_STATUS = 0
	Begin
		select @QRID=QuoteRequestID From QuoteGroups where Id = @GBID 
		select @OPID= OpportunityID from QuoteRequests where QuoteRequestID = @QRID

		print 'Qutoe Group ID ' + cast( @GBID as varchar) + ' Quote Request ' + cast(@QRID as varchar) + ' Opportunity ID ' + cast(@OPID as varchar)

		IF OBJECT_ID('tempdb..#QuoteGroupIDs') IS NOT NULL
			DROP TABLE #QuoteGroupIDs

		select qg.Id as QuoteGroupID 
		  INTO #QuoteGroupIDs 
		  from QuoteGroups QG	  
		   join QuoteRequests QR on QG.QuoteRequestID = QR.QuoteRequestID
		   where OpportunityID = @OPID

		delete from Transportations where QuoteGroupId in ( Select QuoteGroupID from #QuoteGroupIDs )
		delete from QuoteToResultsMappers where QuoteGroupId in ( Select QuoteGroupID from #QuoteGroupIDs )
		update QuoteGroups set QuoteRequestTicketId = null
		delete from FlightItineraries where QuoteGroupId in ( Select QuoteGroupID from #QuoteGroupIDs )
		delete from QuoteRequestResorts where QuoteGroupId in ( Select QuoteGroupID from #QuoteGroupIDs )
		delete from Leg where QuoteGroupId in ( Select QuoteGroupID from #QuoteGroupIDs )
		delete from Staging_Flights where QuoteGroupId in ( Select QuoteGroupID from #QuoteGroupIDs )
		delete from Staging_HotelRates where HotelStagingID in ( select HotelStagingID from Staging_Hotels where QuoteGroupId in ( Select QuoteGroupID from #QuoteGroupIDs ) )
		delete from Staging_Hotels where QuoteGroupId in ( Select QuoteGroupID from #QuoteGroupIDs )
		delete from ClientViews where QuoteGroupId in ( Select QuoteGroupID from #QuoteGroupIDs )
		delete from QuoteGroups where ID in ( Select QuoteGroupID from #QuoteGroupIDs )
		delete from FilteredAccommodations;
		delete from QuoteRequests where QuoteRequestID in ( select QuoteRequestID from QuoteRequests where OpportunityID = @OPID )
		delete from UserMaps where OpportunityID =  @OPID
		delete from Tasks where OpportunityID =  @OPID
		delete from Notes where OpportunityId = @OPID
		delete from booking where TripID = @OPID
		delete from Opportunities where Id =  @OPID
		drop table #QuoteGroupIDs
		FETCH NEXT FROM Enum INTO @GBID
	End
	Close Enum
	DEALLOCATE  Enum

	delete from Booking
	delete from UserMaps
	delete from Tasks
	delete from Notes
	delete from Invoice
	delete from QuoteRequests
	delete from Opportunities
End
