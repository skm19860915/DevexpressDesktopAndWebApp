declare @AgentEmail as varchar(50)
declare @AgentFirstName as varchar(50)
declare @CompanyId as int

set @AgentEmail = 'joyce@eze2travel.com'
set @AgentFirstName = 'Joyce'
select @CompanyId = id from Companies where Name = 'Eze2Travel'

delete AspNetUsers where id = @AgentEmail;
delete from Contact where id = @AgentEmail;

INSERT [dbo].[Contact] ([Id], [Middle], [Suffix], [DOB], [NickName], [AAA_Member], [Address1], [Address2], [City], [State], [ZipCode], [Discriminator], [CommissionRate], [Fee], [Message], [Primary], [RelationshipID], [PendingMerchantApproval], [isMerchant], [Anniversary], [Gender], [MealRequest], [PassportCountry], [PassportExpirationDate], [PassportIssueAgency], [PassportIssueDate], [PassportNumber], [SeatPreferences], [SpecialRequests], [TSANumber], [Title], [ContactSubTypeId], [ContactTypeId], [Age], [HouseHoldId], [MaritalStatus], [PrimaryTeamId], [OwnedById], [CreatedById], [CreatedOn], [UpdatedById], [UpdatedOn], [EmployerId], [Visiblity], [Notes], [Deleted], [GlobalEntryNumber], [First], [Last], [AgentProfileId], [SystemId], [ActivationDate], [ViewMode], [Middle_IsBlank]) VALUES 
(@AgentEmail, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Agent', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, @AgentEmail, N'Eric@eze2travel.com', CAST(N'2021-03-30T17:15:16.9000000' AS DateTime2), NULL, CAST(N'2021-04-15T23:08:01.9166667' AS DateTime2), NULL, 0, NULL, 0, NULL, @AgentFirstName, N'Schedule', NULL, @AgentEmail, NULL, 0, 0);

INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [Middle], [LastName], [Suffix], [LoginEmail], [UserId]) VALUES 
(@AgentEmail, @AgentEmail, upper( @AgentEmail), @AgentEmail, upper(@AgentEmail), 1, N'AQAAAAEAACcQAAAAEEjqVOcQODKIafyZ3qgkJFtAUKDk17UTtS8bt0sryZNsTCsytAtFGDqsjP1RpBmCAw==', N'QIC55DOO5GHA7L6XO2G4QYPKREXFKR3R', N'd7ec3d37-4eb6-4abb-a6fb-2731cc3034ac', NULL, 0, 0, NULL, 1, 0, @AgentFirstName, NULL, N'Schedule', NULL, NULL, @AgentEmail);

insert [AspNetUserRoles] (UserId, RoleId ) values ( @AgentEmail, 'Agent');

insert [TeamMembers] (TeamId, MemberId ) values (4, @AgentEmail )
update Contact set EmployerId = @CompanyId where id = @AgentEmail

go
