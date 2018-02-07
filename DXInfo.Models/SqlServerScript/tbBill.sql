IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbBill]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbBill](
	[iSerial] [bigint] NOT NULL primary key,
	[iAssID] [bigint] NULL,
	[vcCardID] [varchar](10) NOT NULL,
	[nTRate] [numeric](8, 2) NULL,
	[nFee] [numeric](10, 2) NULL,
	[nPay] [numeric](10, 2) NULL,
	[nBalance] [numeric](10, 2) NULL,
	[iIgValue] [int] NULL,
	[vcConsType] [varchar](10) NULL,
	[vcOperName] [varchar](10) NULL,
	[dtConsDate] [datetime] NULL,
	[vcDeptID] [varchar](5) NULL,
) 
end