--delete from pages;
--delete from Medias;
--delete from Graphics;
--delete from Content;
--delete from emails;
delete from AspNetRoles;
delete from AspNetUserRoles;
delete from aspnetUsers;


ALTER TABLE [dbo].[AspNetUserTokens] DROP CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 11/17/2020 8:37:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserTokens]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 11/17/2020 8:37:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 11/17/2020 8:37:51 AM ******/
DROP INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/17/2020 8:37:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/17/2020 8:37:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 11/17/2020 8:38:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO

INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [Middle], [LastName], [Suffix], [DOB], [NickName], [GE_OR_TSA], [AAA_Member], [Address1], [Address2], [City], [State], [ZipCode], [Discriminator], [CommissionRate], [Fee], [AgentID], [Message], [Primary], [RelationshipID], [PendingMerchantApproval], [isMerchant], [Anniversary], [Gender], [ClientStatus], [Notes], [PassportID], [AddressMapID]) VALUES (N'Agent', N'Agent@yahoo.com', N'AGENT@YAHOO.COM', N'agent@yahoo.com', N'AGENT@YAHOO.COM', 1, N'AQAAAAEAACcQAAAAEEjqVOcQODKIafyZ3qgkJFtAUKDk17UTtS8bt0sryZNsTCsytAtFGDqsjP1RpBmCAw==', N'Q7B3TGWHMDKTMDMQYYWZMUETNSLAMG6X', N'2a081aac-0484-42e7-97b9-35861e1ab459', NULL, 0, 0, NULL, 1, 0, N'Test1', NULL, N'Test2', NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Agent', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [Middle], [LastName], [Suffix], [DOB], [NickName], [GE_OR_TSA], [AAA_Member], [Address1], [Address2], [City], [State], [ZipCode], [Discriminator], [CommissionRate], [Fee], [AgentID], [Message], [Primary], [RelationshipID], [PendingMerchantApproval], [isMerchant], [Anniversary], [Gender], [ClientStatus], [Notes], [PassportID], [AddressMapID]) VALUES (N'eric@eze2travel.com', N'eric@yahoo.com', N'ERIC@EZE2TRAVEL.COM', N'eric@eze2travel.com', N'ERIC@EZE2TRAVEL.COM', 1, N'AQAAAAEAACcQAAAAEEjqVOcQODKIafyZ3qgkJFtAUKDk17UTtS8bt0sryZNsTCsytAtFGDqsjP1RpBmCAw==', N'Q7B3TGWHMDKTMDMQYYWZMUETNSLAMG6X', N'2a081aac-0484-42e7-97b9-35861e1ab459', NULL, 0, 0, NULL, 1, 0, N'Eric', NULL, N'Watson', NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Agent', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [Middle], [LastName], [Suffix], [DOB], [NickName], [GE_OR_TSA], [AAA_Member], [Address1], [Address2], [City], [State], [ZipCode], [Discriminator], [CommissionRate], [Fee], [AgentID], [Message], [Primary], [RelationshipID], [PendingMerchantApproval], [isMerchant], [Anniversary], [Gender], [ClientStatus], [Notes], [PassportID], [AddressMapID]) VALUES (N'silke@eze2travel.com', N'silke@eze2travel.com', N'SILKE@EZE2TRAVEL.COM', N'silke@eze2travel.com', N'SILKE@EZE2TRAVEL.COM', 1, N'AQAAAAEAACcQAAAAEEjqVOcQODKIafyZ3qgkJFtAUKDk17UTtS8bt0sryZNsTCsytAtFGDqsjP1RpBmCAw==', N'Q7B3TGWHMDKTMDMQYYWZMUETNSLAMG6X', N'2a081aac-0484-42e7-97b9-35861e1ab459', NULL, 0, 0, NULL, 1, 0, N'Silke', NULL, N'Eze', NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Agent', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
