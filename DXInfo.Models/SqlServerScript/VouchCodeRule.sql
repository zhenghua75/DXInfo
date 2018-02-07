IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VouchCodeRule]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[VouchCodeRule](
	[VouchType] [varchar](50) NOT NULL primary key,
	[Prefix] [varchar](10) NOT NULL,
	[Middle] [varchar](20) NOT NULL,
	[Suffix] [varchar](20) NOT NULL,
)
end
