IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbFillFee]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbFillFee](
	[iSerial] [bigint] IDENTITY(1,1) NOT NULL,
	[iAssID] [bigint] NULL,
	[vcCardID] [varchar](10) NULL,
	[nFillFee] [numeric](10, 2) NULL,
	[nFillProm] [numeric](10, 2) NULL,
	[nFeeLast] [numeric](10, 2) NULL,
	[nFeeCur] [numeric](10, 2) NULL,
	[dtFillDate] [datetime] NULL,
	[vcComments] [varchar](100) NULL,
	[vcOperName] [varchar](10) NULL,
	[vcDeptID] [varchar](5) NULL,
 CONSTRAINT [PK_TBFILLFEE] PRIMARY KEY CLUSTERED 
(
	[iSerial] ASC
)
) 
end