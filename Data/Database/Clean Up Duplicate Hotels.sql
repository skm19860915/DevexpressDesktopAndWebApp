delete from AmenityMaps where AccommodationID = ( select id from Companies where name is null)
delete from Companies where name is null
exec [RemoveDuplicateResort] 116, 448
exec [RemoveDuplicateResort] 116, 286
exec [RemoveDuplicateResort] 533, 256
exec [RemoveDuplicateResort] 443, 445
exec [RemoveDuplicateResort] 174, 301
exec [RemoveDuplicateResort] 431, 446
exec [RemoveDuplicateResort] 246, 222

exec [RemoveDuplicateResort] 175, 444
exec [RemoveDuplicateResort] 175, 210

Update Companies set name = ( Select name from Companies where id = 272 ) where id = 117 
exec [RemoveDuplicateResort] 117, 272
exec [RemoveDuplicateResort] 119, 269
exec [RemoveDuplicateResort] 108, 317

declare @ResortId as int
set @ResortId= 137
delete from Emails where CompanyId = @ResortId
delete from PhoneNumbers where CompanyId = @ResortId
delete from AmenityMaps where AccommodationID = @ResortId
delete from Companies where Discriminator = 'Hotel' and id = @ResortId
set @ResortId= 138
delete from Emails where CompanyId = @ResortId
delete from PhoneNumbers where CompanyId = @ResortId
delete from AmenityMaps where AccommodationID = @ResortId
delete from Companies where Discriminator = 'Hotel' and id = @ResortId

exec [RemoveDuplicateResort] 90, 92