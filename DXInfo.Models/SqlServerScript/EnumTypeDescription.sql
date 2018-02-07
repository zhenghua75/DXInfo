IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EnumTypeDescription]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[EnumTypeDescription](
	[Code] [varchar](200) NOT NULL,
	[Value] [int] NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Code] ASC,
	[Value] ASC
)
)
end