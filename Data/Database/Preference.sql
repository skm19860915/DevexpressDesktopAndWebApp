select * from AgentAirPortPreferences
insert into AgentAirPortPreferences (AirportID, AgentId, AdultOnly,AllInclusive, Stops_Equals_0, Stops_Equals_1, AdultsOnlySection,PerPersonBudget,TripBudget,TripMinBudget, StarRating ) 
values ( 267, 'eric@eze2travel.com', 1, 1, 0, 0, 0,0,0,2000,4)


select * from AgentAirPortPreferences
insert into FilteredAccommodations (AccommodationID, PreferenceId ) values ( 184, 3)
insert into FilteredAccommodations (AccommodationID, PreferenceId ) values ( 104, 3)
insert into FilteredAccommodations (AccommodationID, PreferenceId ) values ( 90, 3)

update AirPort Set City = 'Cancun' where AirPortID = 267

--select * from AirPort where code = 'CUN'
select * from FilteredAccommodations
delete from AgentAirPortPreferences where id = 9

select Id, Name from Companies where name like '%Haven%'
select Id, Name from Companies where name like '%Valen%'
select Id, Name from Companies where name like '%Xcar%'