IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VouchType]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[VouchType](
	[Code] [varchar](50) NOT NULL primary key,
	[Name] [nvarchar](200) NOT NULL,
)
end

