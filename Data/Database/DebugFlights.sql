--select * from Quotegroups
select C.Name, S.name, TourOperatorID, Price, [when] from QuoteRequestResorts R
Join Companies C on R.ResortId = C.id
join SKUs S on R.ResortRoomTypeID = S.SKUID
where QuoteGroupId = (Select max(id) from QuoteGroups)
--and C.Name like 'secrets akumal%'
and Price = 4827.50
order by C.Name, s.name, Price
--order by Price

select 
max(Id) from QuoteGroups

select * from FlightItineraries where QuoteGroupId = 1301
select * from Leg where QuoteGroupId = 1301 and TourOperatorId = 10


