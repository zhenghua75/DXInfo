IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbBusiLog]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbBusiLog](
	[iSerial] [bigint] IDENTITY(1,1) NOT NULL,
	[iAssID] [bigint] NULL,
	[vcCardID] [varchar](10) NULL,
	[vcOperType] [varchar](5) NULL,
	[vcOperName] [varchar](10) NULL,
	[dtOperDate] [datetime] NULL,
	[vcComments] [varchar](100) NULL,
	[vcDeptID] [varchar](5) NULL,
 CONSTRAINT [PK_TBBUSILOG] PRIMARY KEY CLUSTERED 
(
	[iSerial] ASC
)
)
end
