IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategoryDepts]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[CategoryDepts](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Category] [uniqueidentifier] NOT NULL,
	[Dept] [uniqueidentifier] NOT NULL,
	[Comment] [nvarchar](200) NULL,
)
end