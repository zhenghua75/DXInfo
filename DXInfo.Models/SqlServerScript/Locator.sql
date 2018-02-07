IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Locator]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Locator](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Code] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Warehouse] [uniqueidentifier] NOT NULL,
	[Comment] [nvarchar](max) NULL,
)
end

