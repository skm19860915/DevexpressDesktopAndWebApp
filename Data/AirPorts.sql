
SET IDENTITY_INSERT [dbo].[AirPort] ON 
INSERT [dbo].[AirPort] ([AirPortID], [Code], [Name], [URL], [Address], [City], [State], [County], [ZipCode]) VALUES (1, N'RDU', N'Raleigh-Durham International Airport', 'http://wwww.yahoo.com', '2400 John Brantley Blvd', 'Morrisville', 'NC', 'US', 27560)
INSERT [dbo].[AirPort] ([AirPortID], [Code], [Name], [URL], [Address], [City], [State], [County], [ZipCode]) VALUES (2, N'SFO', N'San Francisco International Airport(SFO)', 'http://www.yahoo.com', '780 McDonnel Road', 'San Francisco', 'CA', 'US', 94128)
INSERT [dbo].[AirPort] ([AirPortID], [Code], [Name], [URL], [Address], [City], [State], [County], [ZipCode]) VALUES (3, N'ATL', N'Atlanta International Airport', 'http://www.yahoo.com', '6000 N Terminal Pkwy Suite 4000', 'Atlanta', 'GA', 'US', 30320)
INSERT [dbo].[AirPort] ([AirPortID], [Code], [Name], [URL], [Address], [City], [State], [County], [ZipCode]) VALUES (4, N'BWI', 'Balitmore Washington International', 'http://www.yahoo.com', '7062 Elm Rd', 'Baltimore', 'MD', 'US', 21240)
INSERT [dbo].[AirPort] ([AirPortID], [Code], [Name], [URL], [Address], [City], [State], [County], [ZipCode]) VALUES (5, N'DTW', 'Detroit Metropolitan Airport (DTW)', 'http://www.yahoo.com', '198 William Rogell Drive', 'Detroit', 'MI', 'US', 48242)
SET IDENTITY_INSERT [dbo].[AirPort] OFF
GO

INSERT INTO [dbo].[Transportations] ([Confirmation] ,[Type] ,[Depart],[Arrive]
           ,[Carrier] ,[Identifer],[QuoteRequestID],[QuoteID],[Discriminator],[DepartureAirPortID],[ArrivalAirPortID]
           ,[Quoted],[TripQuoteID],[Side])
     VALUES
           ('VZY6Bl',0,'3/12/2020','3/4/2020','United Airlines'
           ,'YDU38HDJ'
           ,NULL
           ,<Q
           ,<Discriminator, nvarchar(max),>
           ,<DepartureAirPortID, int,>
           ,<ArrivalAirPortID, int,>
           ,<Quoted, datetime2(7),>
           ,<TripQuoteID, uniqueidentifier,>
           ,<Side, int,>)
GO

