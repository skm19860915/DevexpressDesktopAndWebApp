use [QA];
delete from Emails where CompanyId > 150
delete from PhoneNumbers where CompanyId > 150
delete from AmenityMaps where AccommodationID > 150
delete from QuoteToResultsMappers;
delete from QuoteRequestResorts
delete from Companies where Discriminator = 'Hotel' and id > 150

drop user idcasca;
create user idcasca for Login idcasca with DEFAULT_SCHEMA = QA; 
Exec sp_addrolemember 'db_datareader', 'idcasca'
Exec sp_addrolemember 'db_datawriter', 'idcasca'

select MsgType, count(*) from LogMsgs group by MsgType