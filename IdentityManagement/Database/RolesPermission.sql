CREATE TABLE [dbo].[RolesPermission](
	[RolePermissionId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NULL,
	[PermissionId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_RolesPermission] PRIMARY KEY CLUSTERED 
(
	[RolePermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
