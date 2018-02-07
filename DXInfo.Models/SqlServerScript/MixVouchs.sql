IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MixVouchs]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[MixVouchs](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[MVId] [uniqueidentifier] NOT NULL,
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
	[Memo] [nvarchar](max) NULL,
)
end


