
select * from QuoteRequestResorts
select * from Opportunities

use Dev
delete from tasks;
delete from Booking
delete from QuoteToResultsMappers
delete from QuoteRequestTickets
delete from QuoteRequestResorts
delete from ClientViews;
delete from Transportations
delete from QuoteRequests
delete from UserMaps
delete from Notes
delete from Invoice
delete from Opportunities
delete from Leg
delete from Staging_Flights;
delete from Staging_HotelRates
delete from Staging_Hotels

Select * from Airport where code = 'PUJ'
select * from Countries
select * from Companies where Airportid in ( 278, 415)
update Companies set CountryId = 20 where Airportid in ( 278, 415)
Insert Into Countries values ( 'Dominican Republic', 1, 39)