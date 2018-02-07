IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbConsItemOther]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbConsItemOther](
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
	[vcDeptID] [varchar](5) NOT NULL,
 CONSTRAINT [PK_TBCONSITEMOTHER] PRIMARY KEY CLUSTERED 
(
	[iSerial] ASC,
	[vcGoodsID] ASC,
	[vcDeptID] ASC
)
) 
end
