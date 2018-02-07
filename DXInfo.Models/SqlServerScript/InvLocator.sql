IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InvLocator]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[InvLocator](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[ILDate] [date] NOT NULL,
	[RdFlag] [int] NOT NULL,
	[WhId] [uniqueidentifier] NOT NULL,
	[Locator] [uniqueidentifier] NOT NULL,
	[VenId] [uniqueidentifier] NULL,
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
	[Salesman] [uniqueidentifier] NOT NULL,
	[SourceId] [uniqueidentifier] NULL,
	[SourcesId] [uniqueidentifier] NULL,
	[SourceVouchType] [varchar](50) NOT NULL,
)
end

