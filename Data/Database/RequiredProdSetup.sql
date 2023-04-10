
SET IDENTITY_INSERT [dbo].[WebSrvLogins] ON 
INSERT [dbo].[WebSrvLogins] ([WebSrvLoginID], [AgentId], [TourOperatorID], [UserName], [Password]) VALUES (1, N'silke@eze2travel.com', 10, N'EricWatson', N'Drop!Dead')
INSERT [dbo].[WebSrvLogins] ([WebSrvLoginID], [AgentId], [TourOperatorID], [UserName], [Password]) VALUES (2, N'eric@eze2travel.com', 10, N'EricWatson', N'Drop!Dead')
SET IDENTITY_INSERT [dbo].[WebSrvLogins] OFF
GO


CREATE PROCEDURE [dbo].[ConvertToTrip]
(
    @aOppId int 
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    update Opportunities set Discriminator = 'Trip' where ID =  @aOppId
	select * from Opportunities where Id = @aOppId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteQuoteRequestData]    Script Date: 2/25/2021 6:21:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[DeleteQuoteRequestData]
(
    @aQRId int 
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

	delete from Staging_HotelRates where HotelStagingID in ( select HotelStagingID from staging_hotels where RequestID = @aQRId );
	delete from Staging_Hotels where RequestID = @aQRId;
	select count(*) from Staging_HotelRates;
END

