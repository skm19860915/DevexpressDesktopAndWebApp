Select * from AspNetUsers;

delete from Quotes;
delete from Trips;
delete from BlitzerUser;


Insert into BlitzerUser (ID, Name, FirstName, LastName, EmailConfirmed, Discriminator ) values ( 'Agent1', 'Quote Client', 'Quote', 'Client', 0, 'Agent' )
Insert into BlitzerUser (ID, Name, FirstName, LastName, EmailConfirmed, Discriminator ) values ( 'Lead1', 'Quote Client', 'Quote', 'Client', 0, 'Lead' )
SET IDENTITY_INSERT [dbo].Trips ON 
Insert into Trips (TripID, Name, StartDate, EndDate, AGentID, Balance ) values ( 10, 'Test Trip 1', '8/8/2020', '8/20/2020', 'Agent1', 2243.00);
SET IDENTITY_INSERT [dbo].Trips OFF 


SET IDENTITY_INSERT [dbo].Quotes ON 
insert into Quotes ( QuoteID, TripID, ClientID, [When], DepartureID, ArrivalID, DesitinationID, Amount ) values ( 10, 10, 'Lead1', GetDate(), 1,4, 5, 345.00);
SET IDENTITY_INSERT [dbo].Quotes OFF

select * from Transportations order by TripQuoteID

select * from AirPort
select * from BlitzerUser
select * from Trips
select * from Quotes;