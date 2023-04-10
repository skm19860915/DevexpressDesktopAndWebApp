select C.First, C.Last, CC.Number, CC.CVN, CC.Expiration from Contact C
join CreditCards CC on C.Id = CC.OwnerID
where C.last like 'Mock%'



