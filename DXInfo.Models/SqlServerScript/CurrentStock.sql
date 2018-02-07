IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrentStock]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[CurrentStock](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[WhId] [uniqueidentifier] NOT NULL,
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
	[StopFlag] [bit] NOT NULL DEFAULT ((0)),
	[DisableQuantity] [numeric](24, 9) NOT NULL,
	[DisableNum] [numeric](24, 9) NOT NULL,
	[AvaQuantity] [numeric](24, 9) NOT NULL,
	[AvaNum] [numeric](24, 9) NOT NULL,
	[StopQuantity] [numeric](24, 9) NOT NULL,
	[StopNum] [numeric](24, 9) NOT NULL,
	[TransInQuantity] [numeric](24, 9) NOT NULL,
	[TransInNum] [numeric](24, 9) NOT NULL,
	[TransOutQuantity] [numeric](24, 9) NOT NULL,
	[TransOutNum] [numeric](24, 9) NOT NULL,
	[OutQuantity] [numeric](24, 9) NOT NULL,
	[OutNum] [numeric](24, 9) NOT NULL,
	[InQuantity] [numeric](24, 9) NOT NULL,
	[InNum] [numeric](24, 9) NOT NULL,
 CONSTRAINT [unique_CurrentStock] UNIQUE NONCLUSTERED 
(
	[WhId] ASC,
	[InvId] ASC,
	[Batch] ASC
)
)
end

