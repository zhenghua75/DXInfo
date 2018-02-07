IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RdType]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[RdType](
	[Code] [varchar](50) NOT NULL primary key,
	[Name] [nvarchar](200) NOT NULL,
	[Flag] [int] NOT NULL,
)
end

