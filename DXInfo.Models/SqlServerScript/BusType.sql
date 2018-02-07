IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusType]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[BusType](
	[Code] [varchar](50) NOT NULL primary key,
	[Name] [nvarchar](200) NOT NULL,
	[VouchType] [varchar](50) NOT NULL,
)
end