USE [QA]
GO
ALTER TABLE [dbo].[UserMaps] DROP CONSTRAINT [FK_UserMaps_Opportunities_OpportunityID]
GO
ALTER TABLE [dbo].[UserMaps] DROP CONSTRAINT [FK_UserMaps_Contact_UserID]
GO
ALTER TABLE [dbo].[TripComponents] DROP CONSTRAINT [FK_TripComponents_Transportations_TransportationID]
GO
ALTER TABLE [dbo].[TripComponents] DROP CONSTRAINT [FK_TripComponents_Opportunities_TripID]
GO
ALTER TABLE [dbo].[TripComponents] DROP CONSTRAINT [FK_TripComponents_Companies_AccommodationID]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Tasks_UserStories_UserStoryId]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Tasks_Sprints_SprintId]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Tasks_Opportunities_OpportunityID]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Tasks_Contact_UpdatedById]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Tasks_Contact_TargetContactId]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Tasks_Contact_OwnerID]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Tasks_Contact_LastUpdatedById]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Tasks_Contact_IssuerID]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Tasks_Contact_CreatedById]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Tasks_Companies_TargetCompanyId]
GO
ALTER TABLE [dbo].[QuoteToResultsMappers] DROP CONSTRAINT [FK_QuoteToResultsMappers_QuoteRequestResorts_QuoteRequestResortID]
GO
ALTER TABLE [dbo].[QuoteToResultsMappers] DROP CONSTRAINT [FK_QuoteToResultsMappers_QuoteGroups_QuoteGroupID]
GO
ALTER TABLE [dbo].[QuoteToResultsMappers] DROP CONSTRAINT [FK_QuoteToResultsMappers_FlightItineraries_FlightItineraryId]
GO
ALTER TABLE [dbo].[QuoteSKUs] DROP CONSTRAINT [FK_QuoteSKUs_SKUs_SKUID]
GO
ALTER TABLE [dbo].[QuoteSKUs] DROP CONSTRAINT [FK_QuoteSKUs_Quotes_QuoteID]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [FK_Quotes_SKUs_AccommodationRoomTypeID]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [FK_Quotes_QuoteRequests_QuoteRequestID]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [FK_Quotes_QuoteGroups_QuoteGroupID]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [FK_Quotes_Contact_BookedById]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [FK_Quotes_Companies_TourOperatorID]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [FK_Quotes_Companies_SupplierId]
GO
ALTER TABLE [dbo].[QuoteRequestTourOperatorFilter] DROP CONSTRAINT [FK_QuoteRequestTourOperatorFilter_QuoteRequests_QuoteRequestID]
GO
ALTER TABLE [dbo].[QuoteRequestTourOperatorFilter] DROP CONSTRAINT [FK_QuoteRequestTourOperatorFilter_Companies_TourOperatorID]
GO
ALTER TABLE [dbo].[QuoteRequests] DROP CONSTRAINT [FK_QuoteRequests_Opportunities_OpportunityID]
GO
ALTER TABLE [dbo].[QuoteRequests] DROP CONSTRAINT [FK_QuoteRequests_Contact_AgentId]
GO
ALTER TABLE [dbo].[QuoteRequests] DROP CONSTRAINT [FK_QuoteRequests_AirPort_DestinationAirPortID]
GO
ALTER TABLE [dbo].[QuoteRequests] DROP CONSTRAINT [FK_QuoteRequests_AirPort_DepartureAirPortID3]
GO
ALTER TABLE [dbo].[QuoteRequests] DROP CONSTRAINT [FK_QuoteRequests_AirPort_DepartureAirPortID2]
GO
ALTER TABLE [dbo].[QuoteRequests] DROP CONSTRAINT [FK_QuoteRequests_AirPort_DepartureAirPortID]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] DROP CONSTRAINT [FK_QuoteRequestResorts_SKUs_ResortRoomTypeID]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] DROP CONSTRAINT [FK_QuoteRequestResorts_QuoteGroups_QuoteGroupId]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] DROP CONSTRAINT [FK_QuoteRequestResorts_Companies_TourOperatorID]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] DROP CONSTRAINT [FK_QuoteRequestResorts_Companies_ResortId]
GO
ALTER TABLE [dbo].[QuoteRequestResortFilter] DROP CONSTRAINT [FK_QuoteRequestResortFilter_QuoteRequests_QuoteRequestID]
GO
ALTER TABLE [dbo].[QuoteRequestResortFilter] DROP CONSTRAINT [FK_QuoteRequestResortFilter_Companies_AccommodationID]
GO
ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_Posts_PostToMediaMapper_PToMMapID]
GO
ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_Posts_Posts_ParentPostID]
GO
ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_Posts_Contact_UserID]
GO
ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_Posts_Contact_ApproverID]
GO
ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_Posts_Companies_HotelID]
GO
ALTER TABLE [dbo].[Pages] DROP CONSTRAINT [FK_Pages_Panel_RightPanelID]
GO
ALTER TABLE [dbo].[Pages] DROP CONSTRAINT [FK_Pages_Panel_LeftPanelID]
GO
ALTER TABLE [dbo].[Pages] DROP CONSTRAINT [FK_Pages_PageTypes_PageTypeId]
GO
ALTER TABLE [dbo].[Pages] DROP CONSTRAINT [FK_Pages_Pages_UICountryId]
GO
ALTER TABLE [dbo].[Pages] DROP CONSTRAINT [FK_Pages_Countries_CountryId]
GO
ALTER TABLE [dbo].[Pages] DROP CONSTRAINT [FK_Pages_Content_ContentID]
GO
ALTER TABLE [dbo].[Pages] DROP CONSTRAINT [FK_Pages_Contact_AuthorID]
GO
ALTER TABLE [dbo].[Pages] DROP CONSTRAINT [FK_Pages_Block_MainImageID]
GO
ALTER TABLE [dbo].[Pages] DROP CONSTRAINT [FK_Pages_Block_HeaderImageID]
GO
ALTER TABLE [dbo].[Opportunities] DROP CONSTRAINT [FK_Opportunities_Referrals_ReferralId]
GO
ALTER TABLE [dbo].[Opportunities] DROP CONSTRAINT [FK_Opportunities_Contact_UpdatedById]
GO
ALTER TABLE [dbo].[Opportunities] DROP CONSTRAINT [FK_Opportunities_Contact_CreatedById]
GO
ALTER TABLE [dbo].[Opportunities] DROP CONSTRAINT [FK_Opportunities_Contact_AgentId]
GO
ALTER TABLE [dbo].[Opportunities] DROP CONSTRAINT [FK_Opportunities_AirPort_OutboundAirPortID]
GO
ALTER TABLE [dbo].[Opportunities] DROP CONSTRAINT [FK_Opportunities_AirPort_InboundAirPortID]
GO
ALTER TABLE [dbo].[Notes] DROP CONSTRAINT [FK_Notes_Opportunities_OpportunityId]
GO
ALTER TABLE [dbo].[Notes] DROP CONSTRAINT [FK_Notes_Contact_WriterId]
GO
ALTER TABLE [dbo].[Notes] DROP CONSTRAINT [FK_Notes_Contact_ContactId]
GO
ALTER TABLE [dbo].[Notes] DROP CONSTRAINT [FK_Notes_Companies_CompanyId]
GO
ALTER TABLE [dbo].[NewsFeeds] DROP CONSTRAINT [FK_NewsFeeds_UserStories_UserStoryID]
GO
ALTER TABLE [dbo].[NewsFeeds] DROP CONSTRAINT [FK_NewsFeeds_Tasks_TaskID]
GO
ALTER TABLE [dbo].[NewsFeeds] DROP CONSTRAINT [FK_NewsFeeds_Opportunities_OpportunityID]
GO
ALTER TABLE [dbo].[NewsFeeds] DROP CONSTRAINT [FK_NewsFeeds_NewsFeeds_ParentID]
GO
ALTER TABLE [dbo].[NewsFeeds] DROP CONSTRAINT [FK_NewsFeeds_Contact_ContactId]
GO
ALTER TABLE [dbo].[NewsFeeds] DROP CONSTRAINT [FK_NewsFeeds_Companies_CompanyID]
GO
ALTER TABLE [dbo].[Leg] DROP CONSTRAINT [FK_Leg_QuoteGroups_QuoteGroupId]
GO
ALTER TABLE [dbo].[Leg] DROP CONSTRAINT [FK_Leg_Companies_TourOperatorId]
GO
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_Opportunities_TripID]
GO
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_Contact_ClientId]
GO
ALTER TABLE [dbo].[FlightItineraries] DROP CONSTRAINT [FK_FlightItineraries_Quotes_QuoteID]
GO
ALTER TABLE [dbo].[FlightItineraries] DROP CONSTRAINT [FK_FlightItineraries_QuoteRequests_QuoteRequestID]
GO
ALTER TABLE [dbo].[FlightItineraries] DROP CONSTRAINT [FK_FlightItineraries_QuoteGroups_QuoteGroupId]
GO
ALTER TABLE [dbo].[FlightItineraries] DROP CONSTRAINT [FK_FlightItineraries_Leg_OutBoundLegID]
GO
ALTER TABLE [dbo].[FlightItineraries] DROP CONSTRAINT [FK_FlightItineraries_Leg_InBoundLegID]
GO
ALTER TABLE [dbo].[FlightItineraries] DROP CONSTRAINT [FK_FlightItineraries_Companies_TourOperatorId]
GO
ALTER TABLE [dbo].[DataMaps] DROP CONSTRAINT [FK_DataMaps_Companies_TourOperatorID]
GO
ALTER TABLE [dbo].[Credits] DROP CONSTRAINT [FK_Credits_Contact_ContactId]
GO
ALTER TABLE [dbo].[Credits] DROP CONSTRAINT [FK_Credits_Booking_OriginalBookingId]
GO
ALTER TABLE [dbo].[Credits] DROP CONSTRAINT [FK_Credits_Booking_BookingID]
GO
ALTER TABLE [dbo].[CreditCards] DROP CONSTRAINT [FK_CreditCards_Contact_UpdatedById]
GO
ALTER TABLE [dbo].[CreditCards] DROP CONSTRAINT [FK_CreditCards_Contact_OwnerID]
GO
ALTER TABLE [dbo].[CreditCards] DROP CONSTRAINT [FK_CreditCards_Contact_CreatedById]
GO
ALTER TABLE [dbo].[Countries] DROP CONSTRAINT [FK_Countries_Regions_RegionId]
GO
ALTER TABLE [dbo].[Countries] DROP CONSTRAINT [FK_Countries_Pages_PageId]
GO
ALTER TABLE [dbo].[Cities] DROP CONSTRAINT [FK_Cities_Countries_CountryId]
GO
ALTER TABLE [dbo].[Block] DROP CONSTRAINT [FK_Block_Panel_Tile_PanelId]
GO
ALTER TABLE [dbo].[Block] DROP CONSTRAINT [FK_Block_Panel_PanelId]
GO
ALTER TABLE [dbo].[Block] DROP CONSTRAINT [FK_Block_Pages_Tile_ResortID]
GO
ALTER TABLE [dbo].[Block] DROP CONSTRAINT [FK_Block_Pages_ResortID]
GO
ALTER TABLE [dbo].[Block] DROP CONSTRAINT [FK_Block_Medias_MediaID]
GO
ALTER TABLE [dbo].[Block] DROP CONSTRAINT [FK_Block_Category_CategoryID]
GO
ALTER TABLE [dbo].[Block] DROP CONSTRAINT [FK_Block_BlockToPageMap_BlockToPageMapID]
GO
ALTER TABLE [dbo].[AirPort] DROP CONSTRAINT [FK_AirPort_Countries_CountryId]
GO
ALTER TABLE [dbo].[UserMaps] DROP CONSTRAINT [DF__UserMaps__Primar__6319B466]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [DF__Tasks__Target__5E54FF49]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [DF__Tasks__TaskType__5D60DB10]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [DF__Tasks__PriorityT__5C6CB6D7]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [DF__Tasks__LastUpdat__5F492382]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [DF__Tasks__CreatedOn__603D47BB]
GO
ALTER TABLE [dbo].[QuoteToResultsMappers] DROP CONSTRAINT [DF__QuoteToRe__Exclu__54CB950F]
GO
ALTER TABLE [dbo].[QuoteToResultsMappers] DROP CONSTRAINT [DF__QuoteToRe__Quote__53D770D6]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [DF__Quotes__PackageP__5006DFF2]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [DF__Quotes__Booked__52E34C9D]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [DF__Quotes__Total__4E1E9780]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [DF__Quotes__SubTotal__51EF2864]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [DF__Quotes__ResortPr__4D2A7347]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [DF__Quotes__FlightPr__4C364F0E]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [DF__Quotes__Status__50FB042B]
GO
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [DF__Quotes__Adjustme__4F12BBB9]
GO
ALTER TABLE [dbo].[QuoteRequests] DROP CONSTRAINT [DF__QuoteRequ__Quote__4959E263]
GO
ALTER TABLE [dbo].[QuoteRequests] DROP CONSTRAINT [DF__QuoteRequ__Numbe__467D75B8]
GO
ALTER TABLE [dbo].[QuoteRequests] DROP CONSTRAINT [DF__QuoteRequ__Numbe__4865BE2A]
GO
ALTER TABLE [dbo].[QuoteRequests] DROP CONSTRAINT [DF__QuoteRequ__Numbe__477199F1]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] DROP CONSTRAINT [DF__QuoteRequ__Quote__44952D46]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] DROP CONSTRAINT [DF__QuoteRequ__LandO__4589517F]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] DROP CONSTRAINT [DF__QuoteReque__When__3FD07829]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] DROP CONSTRAINT [DF__QuoteRequ__TourO__43A1090D]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] DROP CONSTRAINT [DF__QuoteRequ__Resor__42ACE4D4]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] DROP CONSTRAINT [DF__QuoteRequ__Resor__41B8C09B]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] DROP CONSTRAINT [DF__QuoteRequ__Price__40C49C62]
GO
ALTER TABLE [dbo].[Pages] DROP CONSTRAINT [DF__Pages__PageTypeI__3A179ED3]
GO
ALTER TABLE [dbo].[Opportunities] DROP CONSTRAINT [DF__Opportuni__OppCl__3552E9B6]
GO
ALTER TABLE [dbo].[Opportunities] DROP CONSTRAINT [DF__Opportuni__Updat__382F5661]
GO
ALTER TABLE [dbo].[Opportunities] DROP CONSTRAINT [DF__Opportuni__Creat__39237A9A]
GO
ALTER TABLE [dbo].[Opportunities] DROP CONSTRAINT [DF__Opportuni__Stage__373B3228]
GO
ALTER TABLE [dbo].[Opportunities] DROP CONSTRAINT [DF__Opportuni__Statu__36470DEF]
GO
ALTER TABLE [dbo].[Leg] DROP CONSTRAINT [DF__Leg__TripTicketI__32767D0B]
GO
ALTER TABLE [dbo].[Leg] DROP CONSTRAINT [DF__Leg__TourOperato__318258D2]
GO
ALTER TABLE [dbo].[Leg] DROP CONSTRAINT [DF__Leg__QuoteGroupI__308E3499]
GO
ALTER TABLE [dbo].[FlightItineraries] DROP CONSTRAINT [DF__QuoteRequ__Extra__4A4E069C]
GO
ALTER TABLE [dbo].[FlightItineraries] DROP CONSTRAINT [DF__QuoteRequ__TourO__4B422AD5]
GO
ALTER TABLE [dbo].[CreditCards] DROP CONSTRAINT [DF__CreditCar__Updat__2CBDA3B5]
GO
ALTER TABLE [dbo].[CreditCards] DROP CONSTRAINT [DF__CreditCar__Creat__2BC97F7C]
GO
ALTER TABLE [dbo].[CreditCards] DROP CONSTRAINT [DF__CreditCar__CardT__2AD55B43]
GO
ALTER TABLE [dbo].[Block] DROP CONSTRAINT [DF__Block__Published__1209AD79]
GO
ALTER TABLE [dbo].[Block] DROP CONSTRAINT [DF__Block__OrderId__11158940]
GO
ALTER TABLE [dbo].[Block] DROP CONSTRAINT [DF__Block__Discrimin__10216507]
GO
ALTER TABLE [dbo].[AirPort] DROP CONSTRAINT [DF__AirPort__Default__0F2D40CE]
GO

delete from Leg;
delete from Booking
delete from Opportunities
delete from ClientViews
delete from Staging_Flights
delete from Staging_HotelRates
delete from Staging_Hotels
delete from QuoteGroups
delete from FlightItineraries
delete from QuoteRequestResorts
delete from QuoteToResultsMappers
delete from Quotes
delete from QuoteRequestResorts
delete from Block
delete from BlockToPageMap
delete from PageToBlockMap
delete from Content
delete from CreditCards
delete from Credits
delete from DataMaps
delete from LogMsgs
delete from Medias
delete from Graphics
delete from NOtes
delete from OppUserMapping
update Companies set PageId = null
update Countries set PageId = null;
delete from ResortRankings
delete from Pages;
delete from Panel;
delete from Tasks;
delete from UserMaps
delete from QuoteRequests
delete from Invoice

GO
ALTER TABLE [dbo].[AirPort] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Default]
GO
ALTER TABLE [dbo].[Block] ADD  DEFAULT (N'') FOR [Discriminator]
GO
ALTER TABLE [dbo].[Block] ADD  DEFAULT ((0)) FOR [OrderId]
GO
ALTER TABLE [dbo].[Block] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Published]
GO
ALTER TABLE [dbo].[CreditCards] ADD  DEFAULT ((0)) FOR [CardType]
GO
ALTER TABLE [dbo].[CreditCards] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedOn]
GO
ALTER TABLE [dbo].[CreditCards] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[FlightItineraries] ADD  DEFAULT ((0)) FOR [TourOperatorId]
GO
ALTER TABLE [dbo].[FlightItineraries] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [ExtraCost]
GO
ALTER TABLE [dbo].[Leg] ADD  DEFAULT ((0)) FOR [QuoteGroupId]
GO
ALTER TABLE [dbo].[Leg] ADD  DEFAULT ((0)) FOR [TourOperatorId]
GO
ALTER TABLE [dbo].[Leg] ADD  DEFAULT ((0)) FOR [TripTicketId]
GO
ALTER TABLE [dbo].[Opportunities] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Opportunities] ADD  DEFAULT ((0)) FOR [Stage]
GO
ALTER TABLE [dbo].[Opportunities] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Opportunities] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[Opportunities] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [OppClosedDate]
GO
ALTER TABLE [dbo].[Pages] ADD  DEFAULT ((0)) FOR [PageTypeId]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [Price]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] ADD  DEFAULT ((0)) FOR [ResortId]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] ADD  DEFAULT ((0)) FOR [ResortRoomTypeID]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] ADD  DEFAULT ((0)) FOR [TourOperatorID]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [When]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] ADD  DEFAULT (CONVERT([bit],(0))) FOR [LandOnly]
GO
ALTER TABLE [dbo].[QuoteRequestResorts] ADD  DEFAULT ((0)) FOR [QuoteGroupId]
GO
ALTER TABLE [dbo].[QuoteRequests] ADD  DEFAULT ((0)) FOR [NumberOfAdults]
GO
ALTER TABLE [dbo].[QuoteRequests] ADD  DEFAULT ((0)) FOR [NumberOfRooms]
GO
ALTER TABLE [dbo].[QuoteRequests] ADD  DEFAULT ((0)) FOR [NumberOfChildren]
GO
ALTER TABLE [dbo].[QuoteRequests] ADD  DEFAULT ((0)) FOR [QuoteType]
GO
ALTER TABLE [dbo].[Quotes] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [Adjustment]
GO
ALTER TABLE [dbo].[Quotes] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Quotes] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [FlightPrice]
GO
ALTER TABLE [dbo].[Quotes] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [ResortPrice]
GO
ALTER TABLE [dbo].[Quotes] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [SubTotal]
GO
ALTER TABLE [dbo].[Quotes] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [Total]
GO
ALTER TABLE [dbo].[Quotes] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Booked]
GO
ALTER TABLE [dbo].[Quotes] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [PackagePrice]
GO
ALTER TABLE [dbo].[QuoteToResultsMappers] ADD  DEFAULT ((0)) FOR [QuoteGroupID]
GO
ALTER TABLE [dbo].[QuoteToResultsMappers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Exclude]
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [LastUpdatedOn]
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT ((0)) FOR [PriorityType]
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT ((0)) FOR [TaskType]
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT ((0)) FOR [Target]
GO
ALTER TABLE [dbo].[UserMaps] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Primary]
GO
ALTER TABLE [dbo].[AirPort]  WITH CHECK ADD  CONSTRAINT [FK_AirPort_Countries_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[AirPort] CHECK CONSTRAINT [FK_AirPort_Countries_CountryId]
GO
ALTER TABLE [dbo].[Block]  WITH CHECK ADD  CONSTRAINT [FK_Block_BlockToPageMap_BlockToPageMapID] FOREIGN KEY([BlockToPageMapID])
REFERENCES [dbo].[BlockToPageMap] ([Id])
GO
ALTER TABLE [dbo].[Block] CHECK CONSTRAINT [FK_Block_BlockToPageMap_BlockToPageMapID]
GO
ALTER TABLE [dbo].[Block]  WITH CHECK ADD  CONSTRAINT [FK_Block_Category_CategoryID] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Block] CHECK CONSTRAINT [FK_Block_Category_CategoryID]
GO
ALTER TABLE [dbo].[Block]  WITH CHECK ADD  CONSTRAINT [FK_Block_Medias_MediaID] FOREIGN KEY([MediaID])
REFERENCES [dbo].[Medias] ([Id])
GO
ALTER TABLE [dbo].[Block] CHECK CONSTRAINT [FK_Block_Medias_MediaID]
GO
ALTER TABLE [dbo].[Block]  WITH CHECK ADD  CONSTRAINT [FK_Block_Pages_ResortID] FOREIGN KEY([ResortID])
REFERENCES [dbo].[Pages] ([Id])
GO
ALTER TABLE [dbo].[Block] CHECK CONSTRAINT [FK_Block_Pages_ResortID]
GO
ALTER TABLE [dbo].[Block]  WITH CHECK ADD  CONSTRAINT [FK_Block_Pages_Tile_ResortID] FOREIGN KEY([Tile_ResortID])
REFERENCES [dbo].[Pages] ([Id])
GO
ALTER TABLE [dbo].[Block] CHECK CONSTRAINT [FK_Block_Pages_Tile_ResortID]
GO
ALTER TABLE [dbo].[Block]  WITH CHECK ADD  CONSTRAINT [FK_Block_Panel_PanelId] FOREIGN KEY([PanelId])
REFERENCES [dbo].[Panel] ([Id])
GO
ALTER TABLE [dbo].[Block] CHECK CONSTRAINT [FK_Block_Panel_PanelId]
GO
ALTER TABLE [dbo].[Block]  WITH CHECK ADD  CONSTRAINT [FK_Block_Panel_Tile_PanelId] FOREIGN KEY([Tile_PanelId])
REFERENCES [dbo].[Panel] ([Id])
GO
ALTER TABLE [dbo].[Block] CHECK CONSTRAINT [FK_Block_Panel_Tile_PanelId]
GO
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_Cities_Countries_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_Countries_CountryId]
GO
ALTER TABLE [dbo].[Countries]  WITH CHECK ADD  CONSTRAINT [FK_Countries_Pages_PageId] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
GO
ALTER TABLE [dbo].[Countries] CHECK CONSTRAINT [FK_Countries_Pages_PageId]
GO
ALTER TABLE [dbo].[Countries]  WITH CHECK ADD  CONSTRAINT [FK_Countries_Regions_RegionId] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([Id])
GO
ALTER TABLE [dbo].[Countries] CHECK CONSTRAINT [FK_Countries_Regions_RegionId]
GO
ALTER TABLE [dbo].[CreditCards]  WITH CHECK ADD  CONSTRAINT [FK_CreditCards_Contact_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[CreditCards] CHECK CONSTRAINT [FK_CreditCards_Contact_CreatedById]
GO
ALTER TABLE [dbo].[CreditCards]  WITH CHECK ADD  CONSTRAINT [FK_CreditCards_Contact_OwnerID] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[CreditCards] CHECK CONSTRAINT [FK_CreditCards_Contact_OwnerID]
GO
ALTER TABLE [dbo].[CreditCards]  WITH CHECK ADD  CONSTRAINT [FK_CreditCards_Contact_UpdatedById] FOREIGN KEY([UpdatedById])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[CreditCards] CHECK CONSTRAINT [FK_CreditCards_Contact_UpdatedById]
GO
ALTER TABLE [dbo].[Credits]  WITH CHECK ADD  CONSTRAINT [FK_Credits_Booking_BookingID] FOREIGN KEY([BookingID])
REFERENCES [dbo].[Booking] ([BookingID])
GO
ALTER TABLE [dbo].[Credits] CHECK CONSTRAINT [FK_Credits_Booking_BookingID]
GO
ALTER TABLE [dbo].[Credits]  WITH CHECK ADD  CONSTRAINT [FK_Credits_Booking_OriginalBookingId] FOREIGN KEY([OriginalBookingId])
REFERENCES [dbo].[Booking] ([BookingID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Credits] CHECK CONSTRAINT [FK_Credits_Booking_OriginalBookingId]
GO
ALTER TABLE [dbo].[Credits]  WITH CHECK ADD  CONSTRAINT [FK_Credits_Contact_ContactId] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Credits] CHECK CONSTRAINT [FK_Credits_Contact_ContactId]
GO
ALTER TABLE [dbo].[DataMaps]  WITH CHECK ADD  CONSTRAINT [FK_DataMaps_Companies_TourOperatorID] FOREIGN KEY([TourOperatorID])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DataMaps] CHECK CONSTRAINT [FK_DataMaps_Companies_TourOperatorID]
GO
ALTER TABLE [dbo].[FlightItineraries]  WITH CHECK ADD  CONSTRAINT [FK_FlightItineraries_Companies_TourOperatorId] FOREIGN KEY([TourOperatorId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[FlightItineraries] CHECK CONSTRAINT [FK_FlightItineraries_Companies_TourOperatorId]
GO
ALTER TABLE [dbo].[FlightItineraries]  WITH CHECK ADD  CONSTRAINT [FK_FlightItineraries_Leg_InBoundLegID] FOREIGN KEY([InBoundLegID])
REFERENCES [dbo].[Leg] ([LegID])
GO
ALTER TABLE [dbo].[FlightItineraries] CHECK CONSTRAINT [FK_FlightItineraries_Leg_InBoundLegID]
GO
ALTER TABLE [dbo].[FlightItineraries]  WITH CHECK ADD  CONSTRAINT [FK_FlightItineraries_Leg_OutBoundLegID] FOREIGN KEY([OutBoundLegID])
REFERENCES [dbo].[Leg] ([LegID])
GO
ALTER TABLE [dbo].[FlightItineraries] CHECK CONSTRAINT [FK_FlightItineraries_Leg_OutBoundLegID]
GO
ALTER TABLE [dbo].[FlightItineraries]  WITH CHECK ADD  CONSTRAINT [FK_FlightItineraries_QuoteGroups_QuoteGroupId] FOREIGN KEY([QuoteGroupId])
REFERENCES [dbo].[QuoteGroups] ([Id])
GO
ALTER TABLE [dbo].[FlightItineraries] CHECK CONSTRAINT [FK_FlightItineraries_QuoteGroups_QuoteGroupId]
GO
ALTER TABLE [dbo].[FlightItineraries]  WITH CHECK ADD  CONSTRAINT [FK_FlightItineraries_QuoteRequests_QuoteRequestID] FOREIGN KEY([QuoteRequestID])
REFERENCES [dbo].[QuoteRequests] ([QuoteRequestID])
GO
ALTER TABLE [dbo].[FlightItineraries] CHECK CONSTRAINT [FK_FlightItineraries_QuoteRequests_QuoteRequestID]
GO
ALTER TABLE [dbo].[FlightItineraries]  WITH CHECK ADD  CONSTRAINT [FK_FlightItineraries_Quotes_QuoteID] FOREIGN KEY([QuoteID])
REFERENCES [dbo].[Quotes] ([QuoteID])
GO
ALTER TABLE [dbo].[FlightItineraries] CHECK CONSTRAINT [FK_FlightItineraries_Quotes_QuoteID]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Contact_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Contact_ClientId]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Opportunities_TripID] FOREIGN KEY([TripID])
REFERENCES [dbo].[Opportunities] ([ID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Opportunities_TripID]
GO
ALTER TABLE [dbo].[Leg]  WITH CHECK ADD  CONSTRAINT [FK_Leg_Companies_TourOperatorId] FOREIGN KEY([TourOperatorId])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Leg] CHECK CONSTRAINT [FK_Leg_Companies_TourOperatorId]
GO
ALTER TABLE [dbo].[Leg]  WITH CHECK ADD  CONSTRAINT [FK_Leg_QuoteGroups_QuoteGroupId] FOREIGN KEY([QuoteGroupId])
REFERENCES [dbo].[QuoteGroups] ([Id])
GO
ALTER TABLE [dbo].[Leg] CHECK CONSTRAINT [FK_Leg_QuoteGroups_QuoteGroupId]
GO
ALTER TABLE [dbo].[NewsFeeds]  WITH CHECK ADD  CONSTRAINT [FK_NewsFeeds_Companies_CompanyID] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[NewsFeeds] CHECK CONSTRAINT [FK_NewsFeeds_Companies_CompanyID]
GO
ALTER TABLE [dbo].[NewsFeeds]  WITH CHECK ADD  CONSTRAINT [FK_NewsFeeds_Contact_ContactId] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[NewsFeeds] CHECK CONSTRAINT [FK_NewsFeeds_Contact_ContactId]
GO
ALTER TABLE [dbo].[NewsFeeds]  WITH CHECK ADD  CONSTRAINT [FK_NewsFeeds_NewsFeeds_ParentID] FOREIGN KEY([ParentID])
REFERENCES [dbo].[NewsFeeds] ([ID])
GO
ALTER TABLE [dbo].[NewsFeeds] CHECK CONSTRAINT [FK_NewsFeeds_NewsFeeds_ParentID]
GO
ALTER TABLE [dbo].[NewsFeeds]  WITH CHECK ADD  CONSTRAINT [FK_NewsFeeds_Opportunities_OpportunityID] FOREIGN KEY([OpportunityID])
REFERENCES [dbo].[Opportunities] ([ID])
GO
ALTER TABLE [dbo].[NewsFeeds] CHECK CONSTRAINT [FK_NewsFeeds_Opportunities_OpportunityID]
GO
ALTER TABLE [dbo].[NewsFeeds]  WITH CHECK ADD  CONSTRAINT [FK_NewsFeeds_Tasks_TaskID] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([Id])
GO
ALTER TABLE [dbo].[NewsFeeds] CHECK CONSTRAINT [FK_NewsFeeds_Tasks_TaskID]
GO
ALTER TABLE [dbo].[NewsFeeds]  WITH CHECK ADD  CONSTRAINT [FK_NewsFeeds_UserStories_UserStoryID] FOREIGN KEY([UserStoryID])
REFERENCES [dbo].[UserStories] ([Id])
GO
ALTER TABLE [dbo].[NewsFeeds] CHECK CONSTRAINT [FK_NewsFeeds_UserStories_UserStoryID]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_Companies_CompanyId]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Contact_ContactId] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_Contact_ContactId]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Contact_WriterId] FOREIGN KEY([WriterId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_Contact_WriterId]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Opportunities_OpportunityId] FOREIGN KEY([OpportunityId])
REFERENCES [dbo].[Opportunities] ([ID])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_Opportunities_OpportunityId]
GO
ALTER TABLE [dbo].[Opportunities]  WITH CHECK ADD  CONSTRAINT [FK_Opportunities_AirPort_InboundAirPortID] FOREIGN KEY([InboundAirPortID])
REFERENCES [dbo].[AirPort] ([AirPortID])
GO
ALTER TABLE [dbo].[Opportunities] CHECK CONSTRAINT [FK_Opportunities_AirPort_InboundAirPortID]
GO
ALTER TABLE [dbo].[Opportunities]  WITH CHECK ADD  CONSTRAINT [FK_Opportunities_AirPort_OutboundAirPortID] FOREIGN KEY([OutboundAirPortID])
REFERENCES [dbo].[AirPort] ([AirPortID])
GO
ALTER TABLE [dbo].[Opportunities] CHECK CONSTRAINT [FK_Opportunities_AirPort_OutboundAirPortID]
GO
ALTER TABLE [dbo].[Opportunities]  WITH CHECK ADD  CONSTRAINT [FK_Opportunities_Contact_AgentId] FOREIGN KEY([AgentId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Opportunities] CHECK CONSTRAINT [FK_Opportunities_Contact_AgentId]
GO
ALTER TABLE [dbo].[Opportunities]  WITH CHECK ADD  CONSTRAINT [FK_Opportunities_Contact_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Opportunities] CHECK CONSTRAINT [FK_Opportunities_Contact_CreatedById]
GO
ALTER TABLE [dbo].[Opportunities]  WITH CHECK ADD  CONSTRAINT [FK_Opportunities_Contact_UpdatedById] FOREIGN KEY([UpdatedById])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Opportunities] CHECK CONSTRAINT [FK_Opportunities_Contact_UpdatedById]
GO
ALTER TABLE [dbo].[Opportunities]  WITH CHECK ADD  CONSTRAINT [FK_Opportunities_Referrals_ReferralId] FOREIGN KEY([ReferralId])
REFERENCES [dbo].[Referrals] ([Id])
GO
ALTER TABLE [dbo].[Opportunities] CHECK CONSTRAINT [FK_Opportunities_Referrals_ReferralId]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Block_HeaderImageID] FOREIGN KEY([HeaderImageID])
REFERENCES [dbo].[Block] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Block_HeaderImageID]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Block_MainImageID] FOREIGN KEY([MainImageID])
REFERENCES [dbo].[Block] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Block_MainImageID]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Contact_AuthorID] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Contact_AuthorID]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Content_ContentID] FOREIGN KEY([ContentID])
REFERENCES [dbo].[Content] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Content_ContentID]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Countries_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Countries_CountryId]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Pages_UICountryId] FOREIGN KEY([UICountryId])
REFERENCES [dbo].[Pages] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Pages_UICountryId]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_PageTypes_PageTypeId] FOREIGN KEY([PageTypeId])
REFERENCES [dbo].[PageTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_PageTypes_PageTypeId]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Panel_LeftPanelID] FOREIGN KEY([LeftPanelID])
REFERENCES [dbo].[Panel] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Panel_LeftPanelID]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Panel_RightPanelID] FOREIGN KEY([RightPanelID])
REFERENCES [dbo].[Panel] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Panel_RightPanelID]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Companies_HotelID] FOREIGN KEY([HotelID])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Companies_HotelID]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Contact_ApproverID] FOREIGN KEY([ApproverID])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Contact_ApproverID]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Contact_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Contact_UserID]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Posts_ParentPostID] FOREIGN KEY([ParentPostID])
REFERENCES [dbo].[Posts] ([PostID])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Posts_ParentPostID]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_PostToMediaMapper_PToMMapID] FOREIGN KEY([PToMMapID])
REFERENCES [dbo].[PostToMediaMapper] ([Id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_PostToMediaMapper_PToMMapID]
GO
ALTER TABLE [dbo].[QuoteRequestResortFilter]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequestResortFilter_Companies_AccommodationID] FOREIGN KEY([AccommodationID])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuoteRequestResortFilter] CHECK CONSTRAINT [FK_QuoteRequestResortFilter_Companies_AccommodationID]
GO
ALTER TABLE [dbo].[QuoteRequestResortFilter]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequestResortFilter_QuoteRequests_QuoteRequestID] FOREIGN KEY([QuoteRequestID])
REFERENCES [dbo].[QuoteRequests] ([QuoteRequestID])
GO
ALTER TABLE [dbo].[QuoteRequestResortFilter] CHECK CONSTRAINT [FK_QuoteRequestResortFilter_QuoteRequests_QuoteRequestID]
GO
ALTER TABLE [dbo].[QuoteRequestResorts]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequestResorts_Companies_ResortId] FOREIGN KEY([ResortId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[QuoteRequestResorts] CHECK CONSTRAINT [FK_QuoteRequestResorts_Companies_ResortId]
GO
ALTER TABLE [dbo].[QuoteRequestResorts]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequestResorts_Companies_TourOperatorID] FOREIGN KEY([TourOperatorID])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuoteRequestResorts] CHECK CONSTRAINT [FK_QuoteRequestResorts_Companies_TourOperatorID]
GO
ALTER TABLE [dbo].[QuoteRequestResorts]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequestResorts_QuoteGroups_QuoteGroupId] FOREIGN KEY([QuoteGroupId])
REFERENCES [dbo].[QuoteGroups] ([Id])
GO
ALTER TABLE [dbo].[QuoteRequestResorts] CHECK CONSTRAINT [FK_QuoteRequestResorts_QuoteGroups_QuoteGroupId]
GO
ALTER TABLE [dbo].[QuoteRequestResorts]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequestResorts_SKUs_ResortRoomTypeID] FOREIGN KEY([ResortRoomTypeID])
REFERENCES [dbo].[SKUs] ([SKUID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuoteRequestResorts] CHECK CONSTRAINT [FK_QuoteRequestResorts_SKUs_ResortRoomTypeID]
GO
ALTER TABLE [dbo].[QuoteRequests]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequests_AirPort_DepartureAirPortID] FOREIGN KEY([DepartureAirPortID])
REFERENCES [dbo].[AirPort] ([AirPortID])
GO
ALTER TABLE [dbo].[QuoteRequests] CHECK CONSTRAINT [FK_QuoteRequests_AirPort_DepartureAirPortID]
GO
ALTER TABLE [dbo].[QuoteRequests]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequests_AirPort_DepartureAirPortID2] FOREIGN KEY([DepartureAirPortID2])
REFERENCES [dbo].[AirPort] ([AirPortID])
GO
ALTER TABLE [dbo].[QuoteRequests] CHECK CONSTRAINT [FK_QuoteRequests_AirPort_DepartureAirPortID2]
GO
ALTER TABLE [dbo].[QuoteRequests]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequests_AirPort_DepartureAirPortID3] FOREIGN KEY([DepartureAirPortID3])
REFERENCES [dbo].[AirPort] ([AirPortID])
GO
ALTER TABLE [dbo].[QuoteRequests] CHECK CONSTRAINT [FK_QuoteRequests_AirPort_DepartureAirPortID3]
GO
ALTER TABLE [dbo].[QuoteRequests]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequests_AirPort_DestinationAirPortID] FOREIGN KEY([DestinationAirPortID])
REFERENCES [dbo].[AirPort] ([AirPortID])
GO
ALTER TABLE [dbo].[QuoteRequests] CHECK CONSTRAINT [FK_QuoteRequests_AirPort_DestinationAirPortID]
GO
ALTER TABLE [dbo].[QuoteRequests]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequests_Contact_AgentId] FOREIGN KEY([AgentId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[QuoteRequests] CHECK CONSTRAINT [FK_QuoteRequests_Contact_AgentId]
GO
ALTER TABLE [dbo].[QuoteRequests]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequests_Opportunities_OpportunityID] FOREIGN KEY([OpportunityID])
REFERENCES [dbo].[Opportunities] ([ID])
GO
ALTER TABLE [dbo].[QuoteRequests] CHECK CONSTRAINT [FK_QuoteRequests_Opportunities_OpportunityID]
GO
ALTER TABLE [dbo].[QuoteRequestTourOperatorFilter]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequestTourOperatorFilter_Companies_TourOperatorID] FOREIGN KEY([TourOperatorID])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuoteRequestTourOperatorFilter] CHECK CONSTRAINT [FK_QuoteRequestTourOperatorFilter_Companies_TourOperatorID]
GO
ALTER TABLE [dbo].[QuoteRequestTourOperatorFilter]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequestTourOperatorFilter_QuoteRequests_QuoteRequestID] FOREIGN KEY([QuoteRequestID])
REFERENCES [dbo].[QuoteRequests] ([QuoteRequestID])
GO
ALTER TABLE [dbo].[QuoteRequestTourOperatorFilter] CHECK CONSTRAINT [FK_QuoteRequestTourOperatorFilter_QuoteRequests_QuoteRequestID]
GO
ALTER TABLE [dbo].[Quotes]  WITH CHECK ADD  CONSTRAINT [FK_Quotes_Companies_SupplierId] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Quotes] CHECK CONSTRAINT [FK_Quotes_Companies_SupplierId]
GO
ALTER TABLE [dbo].[Quotes]  WITH CHECK ADD  CONSTRAINT [FK_Quotes_Companies_TourOperatorID] FOREIGN KEY([TourOperatorID])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Quotes] CHECK CONSTRAINT [FK_Quotes_Companies_TourOperatorID]
GO
ALTER TABLE [dbo].[Quotes]  WITH CHECK ADD  CONSTRAINT [FK_Quotes_Contact_BookedById] FOREIGN KEY([BookedById])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Quotes] CHECK CONSTRAINT [FK_Quotes_Contact_BookedById]
GO
ALTER TABLE [dbo].[Quotes]  WITH CHECK ADD  CONSTRAINT [FK_Quotes_QuoteGroups_QuoteGroupID] FOREIGN KEY([QuoteGroupID])
REFERENCES [dbo].[QuoteGroups] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Quotes] CHECK CONSTRAINT [FK_Quotes_QuoteGroups_QuoteGroupID]
GO
ALTER TABLE [dbo].[Quotes]  WITH CHECK ADD  CONSTRAINT [FK_Quotes_QuoteRequests_QuoteRequestID] FOREIGN KEY([QuoteRequestID])
REFERENCES [dbo].[QuoteRequests] ([QuoteRequestID])
GO
ALTER TABLE [dbo].[Quotes] CHECK CONSTRAINT [FK_Quotes_QuoteRequests_QuoteRequestID]
GO
ALTER TABLE [dbo].[Quotes]  WITH CHECK ADD  CONSTRAINT [FK_Quotes_SKUs_AccommodationRoomTypeID] FOREIGN KEY([AccommodationRoomTypeID])
REFERENCES [dbo].[SKUs] ([SKUID])
GO
ALTER TABLE [dbo].[Quotes] CHECK CONSTRAINT [FK_Quotes_SKUs_AccommodationRoomTypeID]
GO
ALTER TABLE [dbo].[QuoteSKUs]  WITH CHECK ADD  CONSTRAINT [FK_QuoteSKUs_Quotes_QuoteID] FOREIGN KEY([QuoteID])
REFERENCES [dbo].[Quotes] ([QuoteID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuoteSKUs] CHECK CONSTRAINT [FK_QuoteSKUs_Quotes_QuoteID]
GO
ALTER TABLE [dbo].[QuoteSKUs]  WITH CHECK ADD  CONSTRAINT [FK_QuoteSKUs_SKUs_SKUID] FOREIGN KEY([SKUID])
REFERENCES [dbo].[SKUs] ([SKUID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuoteSKUs] CHECK CONSTRAINT [FK_QuoteSKUs_SKUs_SKUID]
GO
ALTER TABLE [dbo].[QuoteToResultsMappers]  WITH CHECK ADD  CONSTRAINT [FK_QuoteToResultsMappers_FlightItineraries_FlightItineraryId] FOREIGN KEY([FlightItineraryId])
REFERENCES [dbo].[FlightItineraries] ([FlightItineraryId])
GO
ALTER TABLE [dbo].[QuoteToResultsMappers] CHECK CONSTRAINT [FK_QuoteToResultsMappers_FlightItineraries_FlightItineraryId]
GO
ALTER TABLE [dbo].[QuoteToResultsMappers]  WITH CHECK ADD  CONSTRAINT [FK_QuoteToResultsMappers_QuoteGroups_QuoteGroupID] FOREIGN KEY([QuoteGroupID])
REFERENCES [dbo].[QuoteGroups] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuoteToResultsMappers] CHECK CONSTRAINT [FK_QuoteToResultsMappers_QuoteGroups_QuoteGroupID]
GO
ALTER TABLE [dbo].[QuoteToResultsMappers]  WITH CHECK ADD  CONSTRAINT [FK_QuoteToResultsMappers_QuoteRequestResorts_QuoteRequestResortID] FOREIGN KEY([QuoteRequestResortID])
REFERENCES [dbo].[QuoteRequestResorts] ([QuoteRequestResortID])
GO
ALTER TABLE [dbo].[QuoteToResultsMappers] CHECK CONSTRAINT [FK_QuoteToResultsMappers_QuoteRequestResorts_QuoteRequestResortID]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Companies_TargetCompanyId] FOREIGN KEY([TargetCompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Companies_TargetCompanyId]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Contact_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Contact_CreatedById]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Contact_IssuerID] FOREIGN KEY([IssuerID])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Contact_IssuerID]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Contact_LastUpdatedById] FOREIGN KEY([LastUpdatedById])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Contact_LastUpdatedById]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Contact_OwnerID] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Contact_OwnerID]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Contact_TargetContactId] FOREIGN KEY([TargetContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Contact_TargetContactId]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Contact_UpdatedById] FOREIGN KEY([UpdatedById])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Contact_UpdatedById]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Opportunities_OpportunityID] FOREIGN KEY([OpportunityID])
REFERENCES [dbo].[Opportunities] ([ID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Opportunities_OpportunityID]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Sprints_SprintId] FOREIGN KEY([SprintId])
REFERENCES [dbo].[Sprints] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Sprints_SprintId]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_UserStories_UserStoryId] FOREIGN KEY([UserStoryId])
REFERENCES [dbo].[UserStories] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_UserStories_UserStoryId]
GO
ALTER TABLE [dbo].[TripComponents]  WITH CHECK ADD  CONSTRAINT [FK_TripComponents_Companies_AccommodationID] FOREIGN KEY([AccommodationID])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TripComponents] CHECK CONSTRAINT [FK_TripComponents_Companies_AccommodationID]
GO
ALTER TABLE [dbo].[TripComponents]  WITH CHECK ADD  CONSTRAINT [FK_TripComponents_Opportunities_TripID] FOREIGN KEY([TripID])
REFERENCES [dbo].[Opportunities] ([ID])
GO
ALTER TABLE [dbo].[TripComponents] CHECK CONSTRAINT [FK_TripComponents_Opportunities_TripID]
GO
ALTER TABLE [dbo].[TripComponents]  WITH CHECK ADD  CONSTRAINT [FK_TripComponents_Transportations_TransportationID] FOREIGN KEY([TransportationID])
REFERENCES [dbo].[Transportations] ([TransportationID])
GO
ALTER TABLE [dbo].[TripComponents] CHECK CONSTRAINT [FK_TripComponents_Transportations_TransportationID]
GO
ALTER TABLE [dbo].[UserMaps]  WITH CHECK ADD  CONSTRAINT [FK_UserMaps_Contact_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[UserMaps] CHECK CONSTRAINT [FK_UserMaps_Contact_UserID]
GO
ALTER TABLE [dbo].[UserMaps]  WITH CHECK ADD  CONSTRAINT [FK_UserMaps_Opportunities_OpportunityID] FOREIGN KEY([OpportunityID])
REFERENCES [dbo].[Opportunities] ([ID])
GO
ALTER TABLE [dbo].[UserMaps] CHECK CONSTRAINT [FK_UserMaps_Opportunities_OpportunityID]
GO
