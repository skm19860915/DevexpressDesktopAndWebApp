exec sp_fkeys 'Hotel'

alter Table [AccommodationRoomTypes]  DROP CONSTRAINT FK_AccommodationRoomTypes_Hotel_AccommodiationID;
ALTER TABLE [AmenityMaps] DROP CONSTRAINT [FK_AmenityMaps_Hotel_AccommodationID];
ALTER TABLE [Booking] DROP CONSTRAINT [FK_Booking_Companies_TourOperatorID];
ALTER TABLE [FilteredAccommodations] DROP CONSTRAINT [FK_FilteredAccommodations_Hotel_AccommodationID];
ALTER TABLE [Paragraph] DROP CONSTRAINT [FK_Paragraph_Hotel_HotelAccommodationID];
ALTER TABLE [Posts] DROP CONSTRAINT [FK_Posts_Hotel_HotelID];
ALTER TABLE [QuoteRequestResortFilter] DROP CONSTRAINT [FK_QuoteRequestResortFilter_Hotel_AccommodationID];
ALTER TABLE [QuoteRequestResorts] DROP CONSTRAINT [FK_QuoteRequestResorts_Hotel_HotelAccommodationID];
ALTER TABLE [QuoteRequestResorts] DROP CONSTRAINT [FK_QuoteRequestResorts_Hotel_ResortId];
ALTER TABLE [Quotes] DROP CONSTRAINT [FK_Quotes_Hotel_AccommodationID];
ALTER TABLE [TripComponents] DROP CONSTRAINT [FK_TripComponents_Hotel_AccommodationID];