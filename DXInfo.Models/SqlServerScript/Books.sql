IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Books]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Books](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Period] [uniqueidentifier] NOT NULL,
	[WhId] [uniqueidentifier] NOT NULL,
	[InvId] [uniqueidentifier] NOT NULL,
	[MainUnit] [uniqueidentifier] NOT NULL,
	[STUnit] [uniqueidentifier] NOT NULL,
	[ExchRate] [numeric](24, 9) NOT NULL,
	[InQuantity] [numeric](24, 9) NOT NULL,
	[OutQuantity] [numeric](24, 9) NOT NULL,
	[Quantity] [numeric](24, 9) NOT NULL,
	[InNum] [numeric](24, 9) NOT NULL,
	[OutNum] [numeric](24, 9) NOT NULL,
	[Num] [numeric](24, 9) NOT NULL,
	[Batch] [varchar](50) NULL,
	[Price] [numeric](24, 9) NOT NULL,
	[InAmount] [numeric](24, 9) NOT NULL,
	[OutAmount] [numeric](24, 9) NOT NULL,
	[Amount] [numeric](24, 9) NOT NULL,
	[MadeDate] [date] NULL,
	[ShelfLife] [int] NULL,
	[ShelfLifeType] [int] NULL,
	[InvalidDate] [date] NULL,
	[Locator] [uniqueidentifier] NULL,
	[SourceId] [uniqueidentifier] NULL,
)
end