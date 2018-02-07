IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbBusiLogHis]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbBusiLogHis](
	[iSerial] [bigint] NOT NULL,
	[iAssID] [bigint] NULL,
	[vcCardID] [varchar](10) NULL,
	[vcOperType] [varchar](5) NULL,
	[vcOperName] [varchar](10) NULL,
	[dtOperDate] [datetime] NULL,
	[vcComments] [varchar](100) NULL,
	[vcDeptID] [varchar](5) NULL,
 CONSTRAINT [PK_TBBUSILOGHIS] PRIMARY KEY CLUSTERED 
(
	[iSerial] ASC
)
)
end