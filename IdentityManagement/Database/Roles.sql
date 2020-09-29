
CREATE TABLE [dbo].[Roles](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](50) NULL,
	[RoleDesc] [varchar](100) NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ROLES] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'0019fd51-7130-4cdd-ae72-16124c21c1dd', N'ONLINE_WEIGHING', N'Online Weighing', 1, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'0276b568-63c5-4435-a6ae-5eb4054c7e00', N'MANUAL_WEIGHING', N'Manual Weighing', 2, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'03bae33a-5700-4529-8c3e-077674f241db', N'MODIFY_PENDING', N'Modify Pending Transaction', 3, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'129fdabb-740f-4d60-9030-0449a06e45a6', N'REPRINT_PENDING', N'Reprint Weigh-In Ticket', 4, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'15fab9cf-ab1e-442a-bf21-576e3c7d908d', N'VIEW_COMPLETED', N'View Completed Transaction', 5, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'169d8092-2785-4ff4-9f50-8aa8b05698d0', N'MODIFY_COMPLETED', N'Modify Completed Transaction', 6, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'19d280e1-c755-4530-810a-77fee5d504e5', N'REPRINT_COMPLETED', N'Reprint Weigh-out/Whole Ticket', 7, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'1e1505ba-acb0-4219-a931-d0cfc07e0a3e', N'VIEW_FILE_MAINTENANCE', N'View File Maintenance Records', 8, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'20e2ab32-7ba9-4ca5-abf1-3e88a929025a', N'MODIFY_FILE_MAINTENANCE', N'Modify File Maintenance Records', 9, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'234624ba-b8a0-44c6-9125-d9bb82d79631', N'VIEW_REPORTING', N'View Reporting', 10, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'24afcc37-7e7c-48c8-a952-1381de652849', N'PRINT_REPORTING', N'Print Reporting', 11, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'27ec4ef7-9fd2-4c96-ac46-29aa582f82b8', N'EXPORT_REPORTING', N'Export Reporting', 12, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'29648cd8-90ee-4f50-82e4-e2cd16afd032', N'VIEW_SETTINGS', N'View Settings', 13, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'32d50e51-cf26-434d-91b5-4a0beb19bc86', N'MODIFY_SETTINGS', N'Modify Settings', 14, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'3d4ef647-6672-4ee2-bdaa-19867401bd19', N'CLEAR_LOGS', N'Clear Logs', 15, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'40c4c612-6a41-4dfd-a32f-3cefbe7b04f5', N'CLEAR_LOGS', N'Clear Logs', 16, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'4a9710b2-8f72-4b1a-ae7b-0c8c281a0290', N'VIEW_USER_ACCOUNTS', N'View User Accounts', 17, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'4fe70cf5-db1a-49b9-b7a0-99c1e4798a9c', N'ADD_USER_ACCOUNTS', N'Add User ', 18, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'5bcbe333-726c-4579-addf-367da88b08eb', N'MODIFY_USER_ACCOUNTS', N'Modify User ', 19, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'670dac45-1cc8-4710-b0e7-87b6b2771fe3', N'REMOVE_USER_ACCOUNTS', N'Remove User ', 20, 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'86f30dd6-5e2d-4fdf-bc58-608fc36c1830', N'', N'', 21, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'8f67c346-a5b6-4806-ab2d-a82d01d10e41', N'', N'', 22, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'8fe4b5a1-38aa-496e-9b89-74ae07ab4e2b', N'', N'', 23, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'94f9ad5e-bdbc-4655-a74f-e47004063bdd', N'', N'', 24, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'a0135135-88f4-4a40-bc30-208b16ebd087', N'', N'', 25, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'a07d3e90-1083-4cad-86f6-79d102ffb64f', N'', N'', 26, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'ad568eb4-9781-4036-86cb-e7d407c6ded7', N'', N'', 27, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'b21efbde-806f-41dc-a7f0-2d1270cb9c55', N'', N'', 28, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'bf7ad611-62ad-42df-b174-5621925e5840', N'', N'', 29, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'ca2f3cd3-48f6-405a-8000-908e0fe03595', N'', N'', 30, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'cd78dae9-70bc-4fe5-9c5a-015d876f85cd', N'', N'', 31, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'cd9cbe95-1e19-4b17-9c6a-b9540d1e8324', N'', N'', 32, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'd068fb24-22ad-42a0-9e65-b714df3969c8', N'', N'', 33, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'd9cf369c-d462-45c3-a4f0-759406f86745', N'', N'', 34, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'db4d5b7d-7d1d-4874-816d-c0beb9b6105e', N'', N'', 35, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'dd50c004-49d1-4c50-a908-6d8aef831c4f', N'', N'', 36, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'ddffef57-0410-4042-b242-37106dee7ab3', N'', N'', 37, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'de020af3-e1f0-48a9-b213-1ca7a86816cc', N'', N'', 38, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'f972211a-b9f1-4b28-b924-c8ca175d6ba8', N'', N'', 39, 0)
GO
INSERT [dbo].[Roles] ([Id], [Name], [RoleDesc], [DisplayOrder], [IsActive]) VALUES (N'fc049287-29a5-4bf5-acc8-9d05b92df4c1', N'', N'', 40, 0)
GO