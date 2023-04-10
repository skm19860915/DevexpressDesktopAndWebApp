-- Select the Cheapest
declare @Resort1 as int
declare @Resort2 as int
declare @Resort3 as int
declare @Resort4 as int
declare @Avg4 as float
declare @OppId as int 

set @OppId = 3489
set @Resort1 = 342
set @Resort2 = 377
Set @Resort3 = 345
Set @Resort4 = 377

select @Avg4 = avg(price) 
from QuoteGroups QG
join QuoteRequestResorts QRR on QG.Id = QRR.QuoteGroupId
join Companies C on QRR.ResortId = c.Id
join SKUs S on QRR.ResortRoomTypeID = s.SKUID
join QuoteRequests QR on QG.QuoteRequestID = QR.QuoteRequestID
join AirPort A on QR.DestinationAirPortID = A.AirPortID
join Opportunities O on QR.OpportunityID = o.ID
where O.ID = @OppId

 
select * from ( 
select top 1  convert(varchar, QR.DepartureDate, 1) 'Depature', convert(varchar, QR.ReturnDate, 1) as 'Return', TOO.Name as 'Tour Opp', A.Name as Airport,c.name Resort, S.Name as Room, price
from QuoteGroups QG
join QuoteRequestResorts QRR on QG.Id = QRR.QuoteGroupId
join Companies C on QRR.ResortId = c.Id
join Companies TOO on QRR.TourOperatorID = TOO.Id
join SKUs S on QRR.ResortRoomTypeID = s.SKUID
join QuoteRequests QR on QG.QuoteRequestID = QR.QuoteRequestID
join AirPort A on QR.DestinationAirPortID = A.AirPortID
join Opportunities O on QR.OpportunityID = o.ID
where O.ID = @OppId
and price > 200
and C.Id in ( @Resort1, @Resort2, @Resort3, @Resort4)
and s.Name not like '%double%' and  s.Name not like '%garden%' and s.Name not like '%ocean%'
order by 7 
) as Result1
union
select * from ( select top 1 convert(varchar, QR.DepartureDate, 1) 'Depature', convert(varchar, QR.ReturnDate, 1) as 'Return', TOO.Name as 'Tour Opp', A.Name as Airport,c.name Resort, S.Name as Room, price
from QuoteGroups QG
join QuoteRequestResorts QRR on QG.Id = QRR.QuoteGroupId
join Companies C on QRR.ResortId = c.Id
join SKUs S on QRR.ResortRoomTypeID = s.SKUID
join Companies TOO on QRR.TourOperatorID = TOO.Id
join QuoteRequests QR on QG.QuoteRequestID = QR.QuoteRequestID
join AirPort A on QR.DestinationAirPortID = A.AirPortID
join Opportunities O on QR.OpportunityID = o.ID
where O.ID = @OppId
and price > 200
and C.Id in ( @Resort1, @Resort2, @Resort3, @Resort4)
and s.Name not like '%double%' and s.name like '%ocean%'
order by 7 ) as Result2
union
select * from ( select top 1 convert(varchar, QR.DepartureDate, 1) 'Depature', convert(varchar, QR.ReturnDate, 1) as 'Return', TOO.Name as 'Tour Opp', A.Name as Airport,c.name Resort, S.Name as Room, price
from QuoteGroups QG
join QuoteRequestResorts QRR on QG.Id = QRR.QuoteGroupId
join Companies C on QRR.ResortId = c.Id
join SKUs S on QRR.ResortRoomTypeID = s.SKUID
join Companies TOO on QRR.TourOperatorID = TOO.Id
join QuoteRequests QR on QG.QuoteRequestID = QR.QuoteRequestID
join AirPort A on QR.DestinationAirPortID = A.AirPortID
join Opportunities O on QR.OpportunityID = o.ID
where O.ID = @OppId
and price > 200
and c.Id = @Resort3
and s.Name not like '%double%' and s.name like '%ocean%' 
 order by 7) as Result3
union
select * from ( select top 1 convert(varchar, QR.DepartureDate, 1) 'Depature', convert(varchar, QR.ReturnDate, 1) as 'Return', TOO.Name as 'Tour Opp', A.Name as Airport,c.name Resort, S.Name as Room, price
from QuoteGroups QG
join QuoteRequestResorts QRR on QG.Id = QRR.QuoteGroupId
join Companies C on QRR.ResortId = c.Id
join SKUs S on QRR.ResortRoomTypeID = s.SKUID
join QuoteRequests QR on QG.QuoteRequestID = QR.QuoteRequestID
join Companies TOO on QRR.TourOperatorID = TOO.Id
join AirPort A on QR.DestinationAirPortID = A.AirPortID
join Opportunities O on QR.OpportunityID = o.ID
where O.ID = @OppId
and c.Id = @Resort4  ) as Result4
order by 7

