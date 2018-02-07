IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdjustLocatorVouchs]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[AdjustLocatorVouchs](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[ALVId] [uniqueidentifier] NOT NULL,
	[InvId] [uniqueidentifier] NOT NULL,
	[MainUnit] [uniqueidentifier] NOT NULL,
	[STUnit] [uniqueidentifier] NOT NULL,
	[ExchRate] [numeric](24, 9) NOT NULL,
	[Quantity] [numeric](24, 9) NOT NULL,
	[Num] [numeric](24, 9) NOT NULL,
	[Batch] [varchar](50) NULL,
	[Price] [numeric](24, 9) NOT NULL,
	[Amount] [numeric](24, 9) NOT NULL,
	[MadeDate] [date] NULL,
	[ShelfLife] [int] NULL,
	[ShelfLifeType] [int] NULL,
	[InvalidDate] [date] NULL,
	[OutLocator] [uniqueidentifier] NOT NULL,
	[InLocator] [uniqueidentifier] NOT NULL,
	[Memo] [nvarchar](max) NULL,
) 
end
