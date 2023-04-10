/****** Object:  Table [dbo].[Hotel_bk]    Script Date: 2/12/2021 6:45:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotel_bk](
	[AccommodationID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ThumbNail] [nvarchar](max) NULL,
	[HyperLink] [nvarchar](max) NULL,
	[AAPreferredProvider] [bit] NOT NULL,
	[CheckIn] [datetime2](7) NOT NULL,
	[CheckOut] [datetime2](7) NOT NULL,
	[Bedding] [nvarchar](max) NULL,
	[Inclusions] [nvarchar](max) NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[Area] [nvarchar](max) NULL,
	[ZipCode] [nvarchar](max) NULL,
	[Stars] [float] NULL,
	[FoodRating] [float] NULL,
	[RoomRating] [float] NULL,
	[Description] [nvarchar](max) NULL,
	[QuoteRequestID] [int] NULL,
	[Header] [nvarchar](max) NULL,
	[Summary] [nvarchar](max) NULL,
	[Video] [nvarchar](max) NULL,
	[AirPortID] [int] NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
	[BeachRating] [float] NULL,
	[CategoryId] [int] NULL,
	[CityId] [int] NULL,
	[CountryId] [int] NULL,
	[PageId] [int] NULL,
 CONSTRAINT [PK_Hotel_bk] PRIMARY KEY CLUSTERED 
(
	[AccommodationID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Hotel_bk] ON 
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (7, N'Valentin Imperial Riviera Maya', NULL, NULL, 0, CAST(N'2020-12-03T15:00:00.0000000' AS DateTime2), CAST(N'2020-12-03T11:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Resort', NULL, NULL, 1, 2, 2)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (10, N'ATELIER Playa Mujeres', NULL, NULL, 0, CAST(N'2020-12-03T15:00:00.0000000' AS DateTime2), CAST(N'2020-12-03T11:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Resort', NULL, NULL, 1, 2, 3)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (11, N'Hotel_bk Xcaret', NULL, NULL, 0, CAST(N'2020-12-03T15:00:00.0000000' AS DateTime2), CAST(N'2020-12-03T11:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Resort', NULL, NULL, 1, 2, 4)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (12, N'UNICO 20°87° Riviera Maya', NULL, NULL, 0, CAST(N'2020-12-03T15:00:00.0000000' AS DateTime2), CAST(N'2020-12-03T11:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Resort', NULL, NULL, 1, 2, 5)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (13, N'Hyatt Ziva Cancun ', NULL, NULL, 0, CAST(N'2020-12-03T15:00:00.0000000' AS DateTime2), CAST(N'2020-12-03T11:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Resort', NULL, NULL, 1, 2, 6)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (14, N'Haven Riviera Cancun', NULL, NULL, 0, CAST(N'2020-12-03T15:00:00.0000000' AS DateTime2), CAST(N'2020-12-03T11:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Resort', NULL, NULL, 1, 2, 7)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (15, N'Margaritaville Island Reserve Riviera Cancun', NULL, NULL, 0, CAST(N'2020-12-03T15:00:00.0000000' AS DateTime2), CAST(N'2020-12-03T23:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Resort', NULL, NULL, 1, 2, 8)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (16, N'Hilton Playa del Carmen', NULL, NULL, 0, CAST(N'2020-12-03T15:00:00.0000000' AS DateTime2), CAST(N'2020-12-03T11:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Resort', NULL, NULL, 1, 2, 9)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (17, N'Chablé Maroma', NULL, NULL, 0, CAST(N'2020-12-03T15:00:00.0000000' AS DateTime2), CAST(N'2020-12-03T11:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Resort', NULL, NULL, 1, 2, 11)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (18, N'Rosewood Mayakoba', NULL, NULL, 0, CAST(N'2020-12-03T15:00:00.0000000' AS DateTime2), CAST(N'2020-12-03T11:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Resort', NULL, NULL, 1, 2, 12)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (19, N'The Fives Oceanfront Puerto Morelos', NULL, NULL, 0, CAST(N'2020-12-03T15:00:00.0000000' AS DateTime2), CAST(N'2020-12-03T11:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Resort', NULL, NULL, 1, 2, 13)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (20, N'Banyan Tree Mayakoba', NULL, NULL, 0, CAST(N'2020-12-03T15:00:00.0000000' AS DateTime2), CAST(N'2020-12-03T11:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Resort', NULL, NULL, 1, 2, 14)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (21, N'Grand Velas Riviera Maya', NULL, NULL, 0, CAST(N'2020-12-03T15:00:00.0000000' AS DateTime2), CAST(N'2020-12-03T11:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Resort', NULL, NULL, 1, 2, 17)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (28, N'Hyatt Ziva Cancun', NULL, NULL, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, N'Cancun', NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 267, N'Hotel_bk', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (29, N'Hotel_bk Xcaret Mexico', NULL, NULL, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, N'Riviera Maya', NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 267, N'Hotel_bk', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (30, N'Fairmont Mayakoba', NULL, NULL, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, N'Riviera Maya', NULL, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 267, N'Hotel_bk', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (31, N'Valentin Imperial, Riviera Maya', NULL, NULL, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, N'Riviera Maya', NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 267, N'Hotel_bk', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Hotel_bk] ([AccommodationID], [Name], [ThumbNail], [HyperLink], [AAPreferredProvider], [CheckIn], [CheckOut], [Bedding], [Inclusions], [Address1], [Address2], [State], [Area], [ZipCode], [Stars], [FoodRating], [RoomRating], [Description], [QuoteRequestID], [Header], [Summary], [Video], [AirPortID], [Discriminator], [BeachRating], [CategoryId], [CityId], [CountryId], [PageId]) VALUES (34, N'Dreams Natura', NULL, NULL, 0, CAST(N'2020-12-30T15:00:00.0000000' AS DateTime2), CAST(N'2020-12-30T11:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, N'Riviera Maya', NULL, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 267, N'Resort', NULL, NULL, 1, 2, NULL)
GO
SET IDENTITY_INSERT [dbo].[Hotel_bk] OFF
GO

GO
