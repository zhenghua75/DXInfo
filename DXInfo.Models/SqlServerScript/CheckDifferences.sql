IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckDifferences]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[CheckDifferences](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[DeptId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](24, 2) NOT NULL,
	[More] [decimal](24, 2) NOT NULL,
	[Less] [decimal](24, 2) NOT NULL,
	[DifDate] [date] NOT NULL,
	[OperUserId] [uniqueidentifier] NOT NULL,
	[OperDeptId] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Comment] [nvarchar](200) NULL,
)
end