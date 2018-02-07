IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardTypes]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[CardTypes](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Comment] [nvarchar](200) NULL,
	[IsMoney] [bit] NOT NULL DEFAULT ((1)),
)
end
