IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NameCode]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[NameCode](
	[ID] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Type] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](50) NULL,
	[Comment] [nvarchar](50) NULL,
) 
end

