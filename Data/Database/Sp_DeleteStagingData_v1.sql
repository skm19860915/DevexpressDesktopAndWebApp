USE [Dev]
GO
/****** Object:  StoredProcedure [dbo].[DeleteQuoteRequestData]    Script Date: 2/25/2021 7:24:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[DeleteQuoteRequestData]
(
    @aQGId int 
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

	delete from Staging_HotelRates where HotelStagingID in ( select HotelStagingID from staging_hotels where QuoteGroupId = @aQGId );
	delete from Staging_Hotels where QuoteGroupId = @aQGId;
	delete from Staging_Flights where QuoteGroupId = @aQGId;;
	select count(*) from Staging_HotelRates;
END

select * from QuoteRequestTickets

