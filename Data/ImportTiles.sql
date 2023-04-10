Declare @PageID Int;
Declare @CatID Int;
Declare @GID Int;
Declare @MID Int;

Set @PageID = 11

--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/339.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/340.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/341.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/342.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/343.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/344.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/345.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/346.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/347.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/348.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/349.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/350.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/351.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/352.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/353.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/354.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/355.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/356.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/357.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/358.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/359.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/360.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/361.jpg', 3, N'Photo')
--INSERT [dbo].[Graphics] ([Location], [MediaFormat], [Discriminator]) VALUES (N'https://blitzerblobs.blob.core.windows.net/images/560x460/362.jpg', 3, N'Photo')


Set @GID = 634
Set @MID = 339
Set @CatID = 1
SET IDENTITY_INSERT [dbo].[Medias] ON 
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID, N'Pools',  @GID, @GID, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 1, N'',  @GID + 1, @GID + 1, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 2, N'food8',  @GID + 2, @GID + 2, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 3, N'food8',  @GID + 3, @GID + 3, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 4, N'food8',  @GID + 4, @GID + 4, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 5, N'food8',  @GID + 5, @GID + 5, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 6, N'food8',  @GID + 6, @GID + 6, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 7, N'food8',  @GID + 7, @GID + 7, @CatID,  @PageID)
Set @GID = 642
Set @CatID = 2
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 8, N'Pools',  @GID, @GID, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 9, N'',  @GID + 1, @GID + 1, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 10, N'food8',  @GID + 2, @GID + 2, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 11, N'food8',  @GID + 3, @GID + 3, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 12, N'food8',  @GID + 4, @GID + 4, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 13, N'food8',  @GID + 5, @GID + 5, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 14, N'food8',  @GID + 6, @GID + 6, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 15, N'food8',  @GID + 7, @GID + 7, @CatID,  @PageID)
Set @GID = 650
Set @CatID = 3
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 16, N'Pools',  @GID, @GID, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 17, N'',  @GID + 1, @GID + 1, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 18, N'food8',  @GID + 2, @GID + 2, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 19, N'food8',  @GID + 3, @GID + 3, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 20, N'food8',  @GID + 4, @GID + 4, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 21, N'food8',  @GID + 5, @GID + 5, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 22, N'food8',  @GID + 6, @GID + 6, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 23, N'food8',  @GID + 7, @GID + 7, @CatID,  @PageID)
Set @GID = 658
Set @CatID = 4
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 24, N'Pools',  @GID, @GID, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 25, N'',  @GID + 1, @GID + 1, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 26, N'food8',  @GID + 2, @GID + 2, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 27, N'food8',  @GID + 3, @GID + 3, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 28, N'food8',  @GID + 4, @GID + 4, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 29, N'food8',  @GID + 5, @GID + 5, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 30, N'food8',  @GID + 6, @GID + 6, @CatID,  @PageID)
INSERT [dbo].[Medias] (id, [Title],[Size560x460ID], [ThumbNailID], [CategoryID], [PageID]) VALUES (@MID + 31, N'food8',  @GID + 7, @GID + 7, @CatID,  @PageID)
SET IDENTITY_INSERT [dbo].[Medias] OFF

