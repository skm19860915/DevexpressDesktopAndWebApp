ALTER TABLE [dbo].[Trips] DROP CONSTRAINT [FK_Trips_BlitzerUser_AgentID]
GO
ALTER TABLE [dbo].[TripComponents] DROP CONSTRAINT [FK_TripComponents_Trips_TripID]
GO
ALTER TABLE [dbo].[TripComponents] DROP CONSTRAINT [FK_TripComponents_Transportations_TransportationID]
GO
ALTER TABLE [dbo].[TripComponents] DROP CONSTRAINT [FK_TripComponents_Accommodations_AccommodationID]
GO
ALTER TABLE [dbo].[Transportations] DROP CONSTRAINT [FK_Transportations_Quotes_QuoteID]
GO
ALTER TABLE [dbo].[Transportations] DROP CONSTRAINT [FK_Transportations_QuoteRequests_QuoteRequestID]
GO
ALTER TABLE [dbo].[Transportations] DROP CONSTRAINT [FK_Transportations_Locations_DepartLocationLocationID]
GO
ALTER TABLE [dbo].[Transportations] DROP CONSTRAINT [FK_Transportations_Locations_ArriveLocationLocationID]
GO
ALTER TABLE [dbo].[Transportations] DROP CONSTRAINT [FK_Transportations_AirPort_DepartureAirPortID]
GO
ALTER TABLE [dbo].[Transportations] DROP CONSTRAINT [FK_Transportations_AirPort_ArrivalAirPortID]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [FK_Quotes_Trips_TripID]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [FK_Quotes_BlitzerUser_ClientID]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [FK_Quotes_AirPort_DepartureID]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [FK_Quotes_AirPort_ArrivalID]
GO
ALTER TABLE [dbo].[QuoteRequests] DROP CONSTRAINT [FK_QuoteRequests_AirPort_DestinationAirPortAirPortID]
GO
ALTER TABLE [dbo].[QuoteRequests] DROP CONSTRAINT [FK_QuoteRequests_AirPort_DepartureAirPortAirPortID]
GO
ALTER TABLE [dbo].[Payment] DROP CONSTRAINT [FK_Payment_Booking_BookingID]
GO
ALTER TABLE [dbo].[Payment] DROP CONSTRAINT [FK_Payment_BlitzerUser_PayeeID]
GO
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_Trips_TripID]
GO
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_BlitzerUser_ContactID]
GO
ALTER TABLE [dbo].[HotelStagings] DROP CONSTRAINT [FK_HotelStagings_HotelRateStagings_HotelRateTypesHotelRateStagingID]
GO
ALTER TABLE [dbo].[ContactTrip] DROP CONSTRAINT [FK_ContactTrip_Trips_TripID]
GO
ALTER TABLE [dbo].[ContactTrip] DROP CONSTRAINT [FK_ContactTrip_BlitzerUser_ContactId]
GO
ALTER TABLE [dbo].[Booking] DROP CONSTRAINT [FK_Booking_Trips_TripID]
GO
ALTER TABLE [dbo].[Blobs] DROP CONSTRAINT [FK_Blobs_Ads_AdID]
GO
ALTER TABLE [dbo].[BlitzerUser] DROP CONSTRAINT [FK_BlitzerUser_GetAccountStatuses_StatusID]
GO
ALTER TABLE [dbo].[BlitzerUser] DROP CONSTRAINT [FK_BlitzerUser_BlitzerUser_SalesPersonId]
GO
ALTER TABLE [dbo].[BlitzerUser] DROP CONSTRAINT [FK_BlitzerUser_BlitzerUser_AgentId]
GO
ALTER TABLE [dbo].[AspNetUserTokens] DROP CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_BlitzerUser_AgentId]
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims] DROP CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[Ads] DROP CONSTRAINT [FK_Ads_BlitzerUser_MerchantId]
GO
ALTER TABLE [dbo].[Ads] DROP CONSTRAINT [FK_Ads_Ads_AdID1]
GO
ALTER TABLE [dbo].[Accommodations] DROP CONSTRAINT [FK_Accommodations_Quotes_QuoteID]
GO
ALTER TABLE [dbo].[Accommodations] DROP CONSTRAINT [FK_Accommodations_QuoteRequests_QuoteRequestID]
GO
ALTER TABLE [dbo].[Accommodations] DROP CONSTRAINT [FK_Accommodations_Locations_AddressLocationID]
GO
/****** Object:  Table [dbo].[UserLocations]    Script Date: 4/11/2020 10:21:09 AM ******/
DROP TABLE [dbo].[UserLocations]
GO
/****** Object:  Table [dbo].[UserLocationPreferences]    Script Date: 4/11/2020 10:21:09 AM ******/
DROP TABLE [dbo].[UserLocationPreferences]
GO
/****** Object:  Table [dbo].[Trips]    Script Date: 4/11/2020 10:21:09 AM ******/
DROP TABLE [dbo].[Trips]
GO
/****** Object:  Table [dbo].[TripComponents]    Script Date: 4/11/2020 10:21:09 AM ******/
DROP TABLE [dbo].[TripComponents]
GO
/****** Object:  Table [dbo].[Transportations]    Script Date: 4/11/2020 10:21:09 AM ******/
DROP TABLE [dbo].[Transportations]
GO
/****** Object:  Table [dbo].[SavedAds]    Script Date: 4/11/2020 10:21:09 AM ******/
DROP TABLE [dbo].[SavedAds]
GO
/****** Object:  Table [dbo].[Quotes]    Script Date: 4/11/2020 10:21:09 AM ******/
DROP TABLE [dbo].[Quotes]
GO
/****** Object:  Table [dbo].[QuoteRequests]    Script Date: 4/11/2020 10:21:09 AM ******/
DROP TABLE [dbo].[QuoteRequests]
GO
/****** Object:  Table [dbo].[Phone]    Script Date: 4/11/2020 10:21:10 AM ******/
DROP TABLE [dbo].[Phone]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 4/11/2020 10:21:10 AM ******/
DROP TABLE [dbo].[Payment]
GO
/****** Object:  Table [dbo].[MarketingAd]    Script Date: 4/11/2020 10:21:10 AM ******/
DROP TABLE [dbo].[MarketingAd]
GO
/****** Object:  Table [dbo].[LogMsgs]    Script Date: 4/11/2020 10:21:10 AM ******/
DROP TABLE [dbo].[LogMsgs]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 4/11/2020 10:21:10 AM ******/
DROP TABLE [dbo].[Locations]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 4/11/2020 10:21:10 AM ******/
DROP TABLE [dbo].[Invoice]
GO
/****** Object:  Table [dbo].[HotelStagings]    Script Date: 4/11/2020 10:21:10 AM ******/
DROP TABLE [dbo].[HotelStagings]
GO
/****** Object:  Table [dbo].[HotelRateStagings]    Script Date: 4/11/2020 10:21:10 AM ******/
DROP TABLE [dbo].[HotelRateStagings]
GO
/****** Object:  Table [dbo].[GetAccountStatuses]    Script Date: 4/11/2020 10:21:10 AM ******/
DROP TABLE [dbo].[GetAccountStatuses]
GO
/****** Object:  Table [dbo].[FlightStagings]    Script Date: 4/11/2020 10:21:10 AM ******/
DROP TABLE [dbo].[FlightStagings]
GO
/****** Object:  Table [dbo].[ContactTrip]    Script Date: 4/11/2020 10:21:11 AM ******/
DROP TABLE [dbo].[ContactTrip]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 4/11/2020 10:21:11 AM ******/
DROP TABLE [dbo].[Booking]
GO
/****** Object:  Table [dbo].[Blobs]    Script Date: 4/11/2020 10:21:11 AM ******/
DROP TABLE [dbo].[Blobs]
GO
/****** Object:  Table [dbo].[BlitzerUser]    Script Date: 4/11/2020 10:21:11 AM ******/
DROP TABLE [dbo].[BlitzerUser]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 4/11/2020 10:21:11 AM ******/
DROP TABLE [dbo].[AspNetUserTokens]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 4/11/2020 10:21:11 AM ******/
DROP TABLE [dbo].[AspNetUsers]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 4/11/2020 10:21:11 AM ******/
DROP TABLE [dbo].[AspNetUserRoles]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 4/11/2020 10:21:11 AM ******/
DROP TABLE [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 4/11/2020 10:21:11 AM ******/
DROP TABLE [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 4/11/2020 10:21:11 AM ******/
DROP TABLE [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 4/11/2020 10:21:12 AM ******/
DROP TABLE [dbo].[AspNetRoleClaims]
GO
/****** Object:  Table [dbo].[AirPort]    Script Date: 4/11/2020 10:21:12 AM ******/
DROP TABLE [dbo].[AirPort]
GO
/****** Object:  Table [dbo].[Ads]    Script Date: 4/11/2020 10:21:12 AM ******/
DROP TABLE [dbo].[Ads]
GO
/****** Object:  Table [dbo].[Accommodations]    Script Date: 4/11/2020 10:21:12 AM ******/
DROP TABLE [dbo].[Accommodations]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4/11/2020 10:21:12 AM ******/
DROP TABLE [dbo].[__EFMigrationsHistory]
GO
