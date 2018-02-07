IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbGoods]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbGoods](
	[vcGoodsID] [varchar](10) NOT NULL primary key,
	[vcGoodsName] [varchar](20) NULL,
	[vcSpell] [varchar](10) NULL,
	[nPrice] [numeric](8, 2) NULL,
	[nRate] [numeric](2, 2) NULL,
	[iIgValue] [int] NULL,
	[cNewFlag] [char](1) NULL,
	[vcComments] [varchar](100) NULL,
	[vcGoodsType] [varchar](20) NULL,
)
end
