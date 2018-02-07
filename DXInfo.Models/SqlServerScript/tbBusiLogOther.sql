IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbBusiLogOther]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbBusiLogOther](
	[iSerial] [bigint] NOT NULL,
	[iAssID] [bigint] NULL,
	[vcCardID] [varchar](10) NULL,
	[vcOperType] [varchar](5) NULL,
	[vcOperName] [varchar](10) NULL,
	[dtOperDate] [datetime] NULL,
	[vcComments] [varchar](100) NULL,
	[vcDeptID] [varchar](5) NOT NULL,
 CONSTRAINT [PK_TBBUSILOGOTHER] PRIMARY KEY CLUSTERED 
(
	[iSerial] ASC,
	[vcDeptID] ASC
)
)
end