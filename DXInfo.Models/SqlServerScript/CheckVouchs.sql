IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckVouchs]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[CheckVouchs](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[CVId] [uniqueidentifier] NOT NULL,
	[InvId] [uniqueidentifier] NOT NULL,
	[MainUnit] [uniqueidentifier] NOT NULL,
	[STUnit] [uniqueidentifier] NOT NULL,
	[ExchRate] [numeric](24, 9) NOT NULL,
	[Quantity] [numeric](24, 9) NOT NULL,
	[CQuantity] [numeric](24, 9) NOT NULL,
	[AddInQuantity] [numeric](24, 9) NOT NULL,
	[AddOutQuantity] [numeric](24, 9) NOT NULL,
	[Num] [numeric](24, 9) NOT NULL,
	[CNum] [numeric](24, 9) NOT NULL,
	[AddInNum] [numeric](24, 9) NOT NULL,
	[AddOutNum] [numeric](24, 9) NOT NULL,
	[Price] [numeric](24, 9) NOT NULL,
	[Amount] [numeric](24, 9) NOT NULL,
	[CAmount] [numeric](24, 9) NOT NULL,
	[AddInAmount] [numeric](24, 9) NOT NULL,
	[AddOutAmount] [numeric](24, 9) NOT NULL,
	[Batch] [varchar](50) NULL,
	[MadeDate] [date] NULL,
	[ShelfLife] [int] NULL,
	[ShelfLifeType] [int] NULL,
	[InvalidDate] [date] NULL,
	[Locator] [uniqueidentifier] NOT NULL,
	[Memo] [nvarchar](max) NULL,
)
end

