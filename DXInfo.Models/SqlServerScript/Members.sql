IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Members]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Members](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[MemberName] [nvarchar](200) NULL,
	[IdCard] [nvarchar](200) NULL,
	[LinkPhone] [nvarchar](200) NULL,
	[LinkAddress] [nvarchar](200) NULL,
	[Email] [nvarchar](200) NULL,
	[MemberType] [uniqueidentifier] NULL,
	[Comments] [nvarchar](200) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[DeptId] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifyUserId] [uniqueidentifier] NULL,
	[ModifyDeptId] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[Birthday] [datetime] NULL,
	[Sex] [nvarchar](50) NULL,
)
end

