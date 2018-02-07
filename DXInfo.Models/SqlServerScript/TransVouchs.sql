IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TransVouchs]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[TransVouchs](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[TVId] [uniqueidentifier] NOT NULL,
	[InvId] [uniqueidentifier] NOT NULL,
	[MainUnit] [uniqueidentifier] NOT NULL,
	[STUnit] [uniqueidentifier] NOT NULL,
	[ExchRate] [numeric](24, 9) NOT NULL,
	[Quantity] [numeric](24, 9) NOT NULL,
	[Num] [numeric](24, 9) NOT NULL,
	[Price] [numeric](24, 9) NOT NULL,
	[Amount] [numeric](24, 9) NOT NULL,
	[Batch] [varchar](50) NULL,
	[MadeDate] [date] NULL,
	[ShelfLife] [int] NULL,
	[ShelfLifeType] [int] NULL,
	[InvalidDate] [date] NULL,
	[Locator] [uniqueidentifier] NULL,
	[SourceId] [uniqueidentifier] NULL,
	[DueQuantity] [numeric](24, 9) NOT NULL DEFAULT ((0)),
	[DueNum] [numeric](24, 9) NOT NULL DEFAULT ((0)),
	[DueAmount] [numeric](24, 9) NOT NULL DEFAULT ((0)),
	[Memo] [nvarchar](max) NULL,
)
end

