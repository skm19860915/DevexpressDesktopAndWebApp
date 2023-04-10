SET IDENTITY_INSERT [dbo].[Ads] ON 
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (1, 0, NULL, N'100 Woodland Pond Dr', NULL, N'Cary ', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (2, 0, NULL, N'4100 Main at North Hills St', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (3, 0, NULL, N'101 Park at N Hills St', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (4, 0, NULL, N'2451 Alumni Dr', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (5, 0, NULL, N'4700 Creedmoor Rd', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (6, 0, NULL, N'500 Fayetteville St', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (7, 0, NULL, N'2100 Hillsborough St', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (8, 0, NULL, N'421 S Salisbury St', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (9, 0, NULL, N'3415 Wake Forest Rd', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (10, 0, NULL, N'10030 Sellona St', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (11, 1, NULL, N'
115 W Morgan St', NULL, N'Raleigh ', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (12, 1, NULL, N'11190 Fun Park Dr', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (13, 1, NULL, N'3311 Capital Blvd', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (15, 1, NULL, N'3100 Wake Forest Rd', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (16, 1, NULL, N'410 Glenwood Ave', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (17, 1, NULL, N'8551 Brier Creek Pkwy', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (18, 1, NULL, N'5604 Departure Dr', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (19, 1, NULL, N'7460 Wake Forest Hwy', NULL, N'Durham', N'NC', N'US', NULL)
GO
INSERT [dbo].[Ads] ([AdID], [AdType], [MerchantId], [Address1], [Address2], [City], [State], [Country], [AdID1]) VALUES (20, 1, NULL, N'3200 Pleasant Union Church Rd', NULL, N'Raleigh', N'NC', N'US', NULL)
GO
SET IDENTITY_INSERT [dbo].[Ads] OFF
GO
SET IDENTITY_INSERT [dbo].[Blobs] ON 
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (1, 0, 1, N'Header 1', N'Description 1', N'https://blitzerblobs.blob.core.windows.net/pictures-ads/Black%20Forest_Boats%20on%20Schluchsee_(c)TMBW_Mende.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (2, 0, 2, N'Camal', N'Descrip 2', N'https://blitzerblobs.blob.core.windows.net/pictures-ads/Camal.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (3, 0, 2, N'Camping', N'Description', N'https://blitzerblobs.blob.core.windows.net/pictures-ads/camping.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (4, 0, 3, N'Canada', N'Description 4', N'https://blitzerblobs.blob.core.windows.net/pictures-ads/Canada.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (5, 0, 3, N'Dubai', N'Description 5', N'https://blitzerblobs.blob.core.windows.net/pictures-ads/Dubai.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (6, 0, 4, N'Hot Air Ballon', N'Description 6', N'https://blitzerblobs.blob.core.windows.net/pictures-ads/HotAirBallons.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (7, 0, 5, N'Kayaking', N'Description 7', N'https://blitzerblobs.blob.core.windows.net/pictures-ads/Kayaking.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (8, 1, 10, N'Meal', N'Desc 8', N'https://blitzerblobs.blob.core.windows.net/pictures-ads/Meal.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (9, 1, 10, N'Relax', N'Desc 9', N'https://blitzerblobs.blob.core.windows.net/pictures-ads/Relax.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (10, 1, 11, N'Parasail', N'Enjoy the ocean coast line', N'https://blitzerblobs.blob.core.windows.net/pictures-germany/Ulm%20Minster_(c)TMBW_Mende.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (11, 1, 11, N'Escape Room', N'Can you escape?', N'https://blitzerblobs.blob.core.windows.net/pictures-ads/SearchBackdrop.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (12, 1, 12, N'Jetski Adventures', N'Enjoy jetskiing the ocean', N'https://blitzerblobs.blob.core.windows.net/pictures-ads/SideOfMountain.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (13, 1, 12, N'Dunns River', N'Climb the Falls', N'https://blitzerblobs.blob.core.windows.net/pictures-ads/Skiing.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (14, 1, 13, N'Defy Gravity', N'How high can you jump?', N'https://blitzerblobs.blob.core.windows.net/pictures-germany/Black%20Forest_Boats%20on%20Schluchsee_(c)TMBW_Mende.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (15, 1, 13, N'ATV', N'Off Road fun', N'https://blitzerblobs.blob.core.windows.net/pictures-germany/CarCountry_(c)TMBW_D%C3%BCpper.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (16, 1, 15, N'Jungle Spa', N'Enjoy a massage in the jungle', N'https://blitzerblobs.blob.core.windows.net/pictures-germany/Cuckoo%20Clock_(c)TMBW_D%C3%BCpper.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (17, 1, 15, N'Cliff Jumping', N'Do you dare to jump?', N'https://blitzerblobs.blob.core.windows.net/pictures-germany/Feldberg%20mountain%20during%20winter_(c)TMBW_Mende.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (19, 1, 16, N'Test 1', NULL, N'https://blitzerblobs.blob.core.windows.net/pictures-ads/Tour.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (20, 1, 16, N'test 1.1', N'Descript 5', N'https://blitzerblobs.blob.core.windows.net/pictures-ads/Dubai.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (21, 1, 17, N'Test 2', NULL, N'https://blitzerblobs.blob.core.windows.net/pictures-germany/Upper%20Danube_(c)TMBW_Mende.png')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (22, 1, 16, N'Pic 3', N'Desc 4', N'https://as01.epimg.net/en/imagenes/2019/09/24/football/1569310945_447431_noticia_normal.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (23, 1, 16, N'Pic 4', N'Desc 5', N'https://www.instagram.com/p/bingo.jpg')
GO
INSERT [dbo].[Blobs] ([BlobID], [Type], [AdID], [Header], [Description], [URL]) VALUES (24, 1, 16, N'Pic 5', N'Desc 6', N'https://media1.s-nbcnews.com/j/newscms/2017_24/2035951/170613-world-soccer-ronaldo-tax-0843_ac911bd65d8945f38c691f2bb1c3c42d.fit-2000w.jpg')
GO
SET IDENTITY_INSERT [dbo].[Blobs] OFF
GO
