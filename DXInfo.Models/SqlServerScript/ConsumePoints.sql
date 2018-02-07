IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ConsumePoints]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[ConsumePoints](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[DeptId] [uniqueidentifier] NULL,
	[Category] [uniqueidentifier] NULL,
	[Amount] [decimal](24, 2) NOT NULL,
	[Point] [decimal](24, 2) NOT NULL,
	[Comment] [nvarchar](200) NULL,
)
end

