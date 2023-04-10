alter PROCEDURE [dbo].[RemoveDuplicateResort]
(
	@ValidRID as int,
	@KillRID as int
)
AS
BEGIN
	declare @ValidWebsite varchar(100);

	select 
		@ValidWebsite =Website
	From
		Companies where Id = @ValidRID

	if @ValidWebsite is null
	Begin
		-- Replace the Name
		update Companies set name = ( select name from Companies where id = @KillRID ) where id = @ValidRID
		-- Replace the WebLink
		update Companies set Website = ( select Website from Companies where id = @KillRID ) where id = @ValidRID
	End
	-- Update SKUS
	update SKUs set ProviderID = @ValidRID where ProviderID = @KillRID

	update Emails set CompanyId = @ValidRID where CompanyId = @KillRID
	update PhoneNumbers set CompanyId = @ValidRID where CompanyId = @KillRID
	update AmenityMaps set AccommodationID = @ValidRID where AccommodationID = @KillRID
	update QuoteRequestResorts set ResortId = @ValidRID where ResortId = @KillRID
	delete from Companies where id = @KillRID
End