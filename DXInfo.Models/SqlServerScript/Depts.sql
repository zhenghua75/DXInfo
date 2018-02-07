IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Depts]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Depts](
	[DeptId] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[ParentDeptId] [uniqueidentifier] NULL,
	[Address] [nvarchar](2000) NULL,
	[DeptCode] [nvarchar](256) NOT NULL,
	[DeptName] [nvarchar](256) NOT NULL,
	[Manager] [uniqueidentifier] NULL,
	[Comment] [nvarchar](200) NULL,
	[OrganizationId] [uniqueidentifier] NULL,
	[IsDeptPrice] [bit] NOT NULL DEFAULT ((0)),
)
end

