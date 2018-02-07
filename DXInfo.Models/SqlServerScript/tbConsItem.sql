IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbConsItem]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbConsItem](
	[iSerial] [bigint] NOT NULL,
	[vcGoodsID] [varchar](10) NOT NULL,
	[iAssID] [bigint] NULL,
	[vcCardID] [varchar](10) NOT NULL,
	[nPrice] [numeric](8, 2) NULL,
	[iCount] [int] NULL,
	[nTRate] [numeric](8, 2) NULL,
	[nFee] [numeric](10, 2) NULL,
	[vcComments] [varchar](50) NULL,
	[cFlag] [char](1) NULL,
	[dtConsDate] [datetime] NULL,
	[vcOperName] [varchar](10) NULL,
	[vcDeptID] [varchar](5) NULL,
 CONSTRAINT [PK_TBCONSITEM] PRIMARY KEY CLUSTERED 
(
	[iSerial] ASC,
	[vcGoodsID] ASC
)
)
end