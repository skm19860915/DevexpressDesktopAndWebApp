Select * from Contact where id = 'eze2travel'
Insert into Contact ( id, Discriminator) values ( 'Eze2Travel', 'Agent')
insert into AgentAirPortPreferences ( AirPortId, agentid, Stops_Equals_0, Stops_Equals_1, AdultOnly, AdultsOnlySection, AllInclusive, PerPersonBudget, TripBudget, TripMinBudget, StarRating ) 
values ( 267, 'Eze2Travel', 0, 0, 1, 0, 1, 0, 0, 0, 4 )

select id from Companies where name like ('%Atelier%')
or name like '%Valentin%'
or name like '%Secrets Mox%'
or name like '%UNICO%'
or name like '%Sensira%'
or name like '%Live Aqua%'
order by id

select * from AgentAirPortPreferences
Insert into FilteredAccommodations ( AccommodationID, PreferenceId ) values ( 106, 1 )
Insert into FilteredAccommodations ( AccommodationID, PreferenceId ) values ( 110, 1 )
Insert into FilteredAccommodations ( AccommodationID, PreferenceId ) values ( 116, 1 )
Insert into FilteredAccommodations ( AccommodationID, PreferenceId ) values ( 175, 1 )
Insert into FilteredAccommodations ( AccommodationID, PreferenceId ) values ( 210, 1 )
Insert into FilteredAccommodations ( AccommodationID, PreferenceId ) values ( 286, 1 )
Insert into FilteredAccommodations ( AccommodationID, PreferenceId ) values ( 287, 1 )
Insert into FilteredAccommodations ( AccommodationID, PreferenceId ) values ( 387, 1 )
Insert into FilteredAccommodations ( AccommodationID, PreferenceId ) values ( 443, 1 )
Insert into FilteredAccommodations ( AccommodationID, PreferenceId ) values ( 444, 1 )
Insert into FilteredAccommodations ( AccommodationID, PreferenceId ) values ( 445, 1 )
Insert into FilteredAccommodations ( AccommodationID, PreferenceId ) values ( 448, 1 )
Insert into FilteredAccommodations ( AccommodationID, PreferenceId ) values ( 531, 1 )

select * from FilteredAccommodations
delete from FilteredAccommodations where PreferenceId = 1

select * from Filters
delete from Filters where filterid = 154
select * from AgentAirPortPreferences

select distinct(ResortId) from QuoteRequestResorts where QuoteGroupId = 2349