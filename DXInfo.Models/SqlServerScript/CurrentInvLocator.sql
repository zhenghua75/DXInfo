IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrentInvLocator]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[CurrentInvLocator](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[WhId] [uniqueidentifier] NOT NULL,
	[Locator] [uniqueidentifier] NOT NULL,
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
 CONSTRAINT [unique_CurrentInvLocator] UNIQUE NONCLUSTERED 
(
	[WhId] ASC,
	[Locator] ASC,
	[InvId] ASC,
	[Batch] ASC
)
)
end

