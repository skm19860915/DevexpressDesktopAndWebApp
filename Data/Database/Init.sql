INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [Middle], [LastName], [Suffix], [DOB], [NickName], [GE_OR_TSA], [AAA_Member], [Address1], [Address2], [City], [State], [ZipCode], [Discriminator], [CommissionRate], [Fee], [AgentID], [Message], [Primary], [RelationshipID], [PendingMerchantApproval], [isMerchant], [Anniversary], [Gender], [ClientStatus], [Notes], [PassportID], [AddressMapID]) VALUES (N'Agent', N'Agent@yahoo.com', N'AGENT@YAHOO.COM', N'agent@yahoo.com', N'AGENT@YAHOO.COM', 1, N'AQAAAAEAACcQAAAAEEjqVOcQODKIafyZ3qgkJFtAUKDk17UTtS8bt0sryZNsTCsytAtFGDqsjP1RpBmCAw==', N'Q7B3TGWHMDKTMDMQYYWZMUETNSLAMG6X', N'2a081aac-0484-42e7-97b9-35861e1ab459', NULL, 0, 0, NULL, 1, 0, N'Test1', NULL, N'Test2', NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Agent', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [Middle], [LastName], [Suffix], [DOB], [NickName], [GE_OR_TSA], [AAA_Member], [Address1], [Address2], [City], [State], [ZipCode], [Discriminator], [CommissionRate], [Fee], [AgentID], [Message], [Primary], [RelationshipID], [PendingMerchantApproval], [isMerchant], [Anniversary], [Gender], [ClientStatus], [Notes], [PassportID], [AddressMapID]) VALUES (N'eric@eze2travel.com', N'eric@eze2travel.com', N'ERIC@EZE2TRAVEL.COM', N'eric@eze2travel.com', N'ERIC@EZE2TRAVEL.COM', 1, N'AQAAAAEAACcQAAAAEEjqVOcQODKIafyZ3qgkJFtAUKDk17UTtS8bt0sryZNsTCsytAtFGDqsjP1RpBmCAw==', N'Q7B3TGWHMDKTMDMQYYWZMUETNSLAMG6X', N'2a081aac-0484-42e7-97b9-35861e1ab459', NULL, 0, 0, NULL, 1, 0, N'Eric', NULL, N'Watson', NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Agent', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [Middle], [LastName], [Suffix], [DOB], [NickName], [GE_OR_TSA], [AAA_Member], [Address1], [Address2], [City], [State], [ZipCode], [Discriminator], [CommissionRate], [Fee], [AgentID], [Message], [Primary], [RelationshipID], [PendingMerchantApproval], [isMerchant], [Anniversary], [Gender], [ClientStatus], [Notes], [PassportID], [AddressMapID]) VALUES (N'silke@eze2travel.com', N'silke@eze2travel.com', N'SILKE@EZE2TRAVEL.COM', N'silke@eze2travel.com', N'SILKE@EZE2TRAVEL.COM', 1, N'AQAAAAEAACcQAAAAEEjqVOcQODKIafyZ3qgkJFtAUKDk17UTtS8bt0sryZNsTCsytAtFGDqsjP1RpBmCAw==', N'Q7B3TGWHMDKTMDMQYYWZMUETNSLAMG6X', N'2a081aac-0484-42e7-97b9-35861e1ab459', NULL, 0, 0, NULL, 1, 0, N'Silke', NULL, N'Eze', NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Agent', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [Middle], [LastName], [Suffix], [DOB], [NickName], [GE_OR_TSA], [AAA_Member], [Address1], [Address2], [City], [State], [ZipCode], [Discriminator], [CommissionRate], [Fee], [AgentID], [Message], [Primary], [RelationshipID], [PendingMerchantApproval], [isMerchant], [Anniversary], [Gender], [ClientStatus], [Notes], [PassportID], [AddressMapID]) VALUES (N'unittest@eze2travel.com', N'unittest@eze2travel.com', N'UNITTEST@EZE2TRAVEL.COM', N'unittest@eze2travel.com', N'UNITTEST@EZE2TRAVEL.COM', 1, N'AQAAAAEAACcQAAAAEEjqVOcQODKIafyZ3qgkJFtAUKDk17UTtS8bt0sryZNsTCsytAtFGDqsjP1RpBmCAw==', N'Q7B3TGWHMDKTMDMQYYWZMUETNSLAMG6X', N'2a081aac-0484-42e7-97b9-35861e1ab459', NULL, 0, 0, NULL, 1, 0, N'Unit', NULL, N'Test', NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Agent', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

SET IDENTITY_INSERT [dbo].[EmailTypes] ON 

INSERT [dbo].[EmailTypes] ([EmailTypeID], [Name]) VALUES (1, N'Personnel')
INSERT [dbo].[EmailTypes] ([EmailTypeID], [Name]) VALUES (2, N'Business')
SET IDENTITY_INSERT [dbo].[EmailTypes] OFF

INSERT [dbo].[Emails] ([EmailTypeID], [Address], [Preferred], [UserId], [BusinessId]) VALUES ( 1, N'eric@eze2travel.com', 1, N'eric@eze2travel.com', NULL)
INSERT [dbo].[Emails] ([EmailTypeID], [Address], [Preferred], [UserId], [BusinessId]) VALUES ( 1, N'silke@eze2travel.com', 1, N'silke@eze2travel.com', NULL)

INSERT INTO [dbo].[AspNetRoles] ([Id] ,[Name]) VALUES ('Admin' ,'Administator')
INSERT INTO [dbo].[AspNetRoles] ([Id] ,[Name]) VALUES ('Agent' ,'Agent')

INSERT INTO [dbo].[AspNetUserRoles]([UserId],[RoleId]) VALUES ('eric@eze2travel.com','Admin')
INSERT INTO [dbo].[AspNetUserRoles]([UserId],[RoleId]) VALUES ('silke@eze2travel.com','Agent')

SET IDENTITY_INSERT [dbo].[Stages] ON 

INSERT [dbo].[Stages] ([StageID], [Description], [Default]) VALUES (1, N'Qualification', 1)
INSERT [dbo].[Stages] ([StageID], [Description], [Default]) VALUES (2, N'Meeting Scheduled', 0)
INSERT [dbo].[Stages] ([StageID], [Description], [Default]) VALUES (3, N'Price Quote', 0)
INSERT [dbo].[Stages] ([StageID], [Description], [Default]) VALUES (4, N'Negotiations', 0)
INSERT [dbo].[Stages] ([StageID], [Description], [Default]) VALUES (5, N'Won', 0)
INSERT [dbo].[Stages] ([StageID], [Description], [Default]) VALUES (6, N'Loss', 0)
SET IDENTITY_INSERT [dbo].[Stages] OFF

SET IDENTITY_INSERT [dbo].[Relationships] ON 
INSERT [dbo].[Relationships] ([RelationshipID], [Description]) VALUES (1, N'Husband')
INSERT [dbo].[Relationships] ([RelationshipID], [Description]) VALUES (2, N'Wife')
INSERT [dbo].[Relationships] ([RelationshipID], [Description]) VALUES (3, N'Son')
INSERT [dbo].[Relationships] ([RelationshipID], [Description]) VALUES (4, N'Daughter')
SET IDENTITY_INSERT [dbo].[Relationships] OFF

SET IDENTITY_INSERT [dbo].[PhoneType] ON 
INSERT [dbo].[PhoneType] ([PhoneTypeID], [Name]) VALUES (1, N'Cell')
INSERT [dbo].[PhoneType] ([PhoneTypeID], [Name]) VALUES (2, N'Home')
INSERT [dbo].[PhoneType] ([PhoneTypeID], [Name]) VALUES (3, N'Office')
SET IDENTITY_INSERT [dbo].[PhoneType] OFF

