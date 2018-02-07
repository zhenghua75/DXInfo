IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STType]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[STType](
	[Code] [varchar](50) NOT NULL primary key,
	[Name] [nvarchar](200) NOT NULL,
	[RdCode] [varchar](50) NOT NULL,
	[IsDefault] [bit] NOT NULL,
)
end


