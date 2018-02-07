IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Warehouse]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Warehouse](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Code] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Dept] [uniqueidentifier] NOT NULL,
	[Principal] [nvarchar](200) NULL,
	[Tele] [nvarchar](200) NULL,
	[Address] [nvarchar](200) NULL,
	[Comment] [nvarchar](max) NULL,
)
end

