IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EnumType]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[EnumType](
	[Code] [varchar](200) NOT NULL primary key,
	[Name] [nvarchar](200) NOT NULL,
	[Comment] [nvarchar](200) NULL,
)
end
