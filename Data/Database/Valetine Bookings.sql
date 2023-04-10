select o.Name as Opportunity, Con.First, con.Last, Ct.Name as 'Tour Operator', c.Name as Hotel, b.BookingNumber, convert(varchar, o.StartDate,1), convert(varchar, o.EndDate, 1)
from Booking B
join Opportunities O on B.TripID = O.ID
join UserMaps U on O.ID = U.OpportunityID
join Contact Con on U.UserID = Con.Id
join Companies C on B.SupplierId = C.Id
join Companies CT on B.TourOperatorID = CT.id
where B.Status = 2 and c.Name like 'Est%'
order by [Tour Operator], o.StartDate
