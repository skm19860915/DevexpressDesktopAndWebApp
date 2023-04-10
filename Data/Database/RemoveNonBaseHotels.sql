delete from Emails where CompanyId > 150
delete from PhoneNumbers where CompanyId > 150
delete from AmenityMaps where AccommodationID > 150
delete from QuoteToResultsMappers;
delete from QuoteRequestResorts
delete from Companies where Discriminator = 'Hotel' and id > 150

select * from Companies where Discriminator = 'Hotel'
--select * from NameReplacements;

RESTORE DATABASE [QA] WITH RECOVERY