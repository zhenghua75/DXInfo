IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RdRecords]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[RdRecords](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[RdId] [uniqueidentifier] NOT NULL,
	[InvId] [uniqueidentifier] NOT NULL,
	[MainUnit] [uniqueidentifier] NOT NULL,
	[STUnit] [uniqueidentifier] NOT NULL,
	[ExchRate] [numeric](24, 9) NOT NULL,
	[LastBanaceQuantity] [numeric](24, 9) NOT NULL,
	[LastBalanceLocatorQuantity] [numeric](24, 9) NOT NULL,
	[Quantity] [numeric](24, 9) NOT NULL,
	[BalanceQuantity] [numeric](24, 9) NOT NULL,
	[BalanceLocatorQuantity] [numeric](24, 9) NOT NULL,
	[LastBalanceNum] [numeric](24, 9) NOT NULL,
	[LastBalanceLocatorNum] [numeric](24, 9) NOT NULL,
	[Num] [numeric](24, 9) NOT NULL,
	[BalanceNum] [numeric](24, 9) NOT NULL,
	[BalanceLocatorNum] [numeric](24, 9) NOT NULL,
	[Price] [numeric](24, 9) NOT NULL,
	[LastBalanceAmount] [numeric](24, 9) NOT NULL,
	[LastBalanceLocatorAmount] [numeric](24, 9) NOT NULL,
	[Amount] [numeric](24, 9) NOT NULL,
	[BalanceAmount] [numeric](24, 9) NOT NULL,
	[BalanceLocatorAmount] [numeric](24, 9) NOT NULL,
	[Batch] [varchar](50) NULL,
	[MadeDate] [date] NULL,
	[ShelfLife] [int] NULL,
	[ShelfLifeType] [int] NULL,
	[InvalidDate] [date] NULL,
	[Locator] [uniqueidentifier] NULL,
	[DueQuantity] [numeric](24, 9) NOT NULL DEFAULT ((0)),
	[DueNum] [numeric](24, 9) NOT NULL DEFAULT ((0)),
	[DueAmount] [numeric](24, 9) NOT NULL DEFAULT ((0)),
	[Memo] [nvarchar](max) NULL,
)
end

