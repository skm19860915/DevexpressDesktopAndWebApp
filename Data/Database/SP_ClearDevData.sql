/****** Object:  StoredProcedure [dbo].[ClearTestData]    Script Date: 11/15/2020 9:52:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--drop procedure [dbo].[ClearTestData];
--go
create procedure [dbo].[ClearDevData]
as
Begin
delete from QuoteToResultsMappers;
delete from QuoteRequestResorts;
delete from AIFilterMaps;
delete from Filters;
delete from Quotes;
delete from Transportations;
delete from AmenityMaps;
delete from QuoteRequestTickets;
delete from QuoteRequests;
delete from Booking;
delete from UserMaps;
delete from notes;
delete from Tasks;
delete from Invoice;
delete from Opportunities;
delete from AspNetUserRoles where RoleId = 'Client'
delete from AspNetUsers where id <> 'eric@eze2travel.com' and id <> 'Eze2Travel'  and id <> 'silke@eze2travel.com'
delete from Leg;
delete from LogMsgs;
delete from Staging_Flights;
delete from Staging_HotelRates;
delete from Staging_Hotels;
delete from CreditCards 
delete from Emails where UserId in ( Select Id from Contact where CreatedOn > '2/27/2021')
delete from Phone where UserId in ( Select Id from Contact where CreatedOn > '2/27/2021')
delete from Contact where CreatedOn > '2/27/2021'
delete from Emails where address like 'public%'

--delete from PageToBlockMap
--delete from BlockToPageMap

--update Block set BlockToPageMapID = null;
--update Pages set HeaderImageID = null;
--update Pages set MainImageID = null;

--delete from Block
--update Countries set PageId = null;
----update Hotel set PageId = null;
--update Medias set PageID = null;
--delete from ResortRankings
--delete from pages

End;