IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Inventory](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Category] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UnitOfMeasure] [uniqueidentifier] NULL,
	[Specs] [nvarchar](50) NULL,
	[Comment] [nvarchar](50) NULL,
	[ImageFileName] [nvarchar](200) NULL,
	[SalePrice] [decimal](24, 2) NOT NULL,
	[SalePrice0] [decimal](24, 2) NOT NULL,
	[SalePrice1] [decimal](24, 2) NOT NULL,
	[SalePrice2] [decimal](24, 2) NOT NULL,
	[SalePoint] [decimal](24, 2) NOT NULL,
	[SalePoint0] [decimal](24, 2) NOT NULL,
	[SalePoint1] [decimal](24, 2) NOT NULL,
	[SalePoint2] [decimal](24, 2) NOT NULL,
	[IsDonate] [bit] NOT NULL DEFAULT ((0)),
	[Stars] [int] NOT NULL DEFAULT ((0)),
	[Feature] [nvarchar](2000) NULL,
	[Dosage] [nvarchar](2000) NULL,
	[Palette] [nvarchar](2000) NULL,
	[EnglishName] [nvarchar](200) NULL,
	[IsRecommend] [bit] NOT NULL DEFAULT ((0)),
	[Printer] [nvarchar](200) NULL,
	[EnglishIntroduce] [nvarchar](2000) NULL,
	[EnglishDosage] [nvarchar](2000) NULL,
	[InvType] [int] NOT NULL DEFAULT ((0)),
	[IsPackage] [bit] NOT NULL DEFAULT ((0)),
	[IsInvalid] [bit] NOT NULL DEFAULT ((0)),
	[Sort] [bigint] NOT NULL DEFAULT ((0)),
	[MeasurementUnitGroup] [uniqueidentifier] NOT NULL DEFAULT ('00000000-0000-0000-0000-000000000000'),
	[MainUnit] [uniqueidentifier] NOT NULL DEFAULT ('00000000-0000-0000-0000-000000000000'),
	[UnitCategory] [int] NOT NULL DEFAULT ((0)),
	[PurchaseUnit] [uniqueidentifier] NULL DEFAULT ('00000000-0000-0000-0000-000000000000'),
	[StockUnit] [uniqueidentifier] NULL DEFAULT ('00000000-0000-0000-0000-000000000000'),
	[ValueType] [int] NOT NULL DEFAULT ((0)),
	[HighStock] [numeric](24, 9) NOT NULL DEFAULT ((0)),
	[LowStock] [numeric](24, 9) NOT NULL DEFAULT ((0)),
	[SecurityStock] [numeric](24, 9) NOT NULL DEFAULT ((0)),
	[Locator] [uniqueidentifier] NOT NULL DEFAULT ('00000000-0000-0000-0000-000000000000'),
	[LastCheckDate] [datetime] NULL,
	[CheckCycle] [int] NOT NULL DEFAULT ((0)),
	[SomeDay] [int] NOT NULL DEFAULT ((0)),
	[IsShelfLife] [bit] NOT NULL DEFAULT ((1)),
	[ShelfLife] [int] NOT NULL DEFAULT ((0)),
	[EarlyWarningDay] [int] NOT NULL DEFAULT ((0)),
	[IsBatch] [bit] NOT NULL DEFAULT ((1)),
	[ShelfLifeType] [int] NOT NULL DEFAULT ((0)),
)
end

if not exists (select * from syscolumns where name='IsSale' and id=OBJECT_ID(N'[dbo].[Inventory]'))
begin
alter table dbo.Inventory add IsSale bit not null default(0)
end

if not exists (select * from syscolumns where name='WhId' and id=OBJECT_ID(N'[dbo].[Inventory]'))
begin
alter table dbo.Inventory add WhId uniqueidentifier null 
end

if not exists (select * from syscolumns where name='Karat' and id=OBJECT_ID(N'[dbo].[Inventory]'))
begin
alter table dbo.Inventory add Karat decimal(24,2) not null default(0)
end

if not exists (select * from syscolumns where name='MetalWeight' and id=OBJECT_ID(N'[dbo].[Inventory]'))
begin
alter table dbo.Inventory add MetalWeight decimal(24,2) not null default(0)
end

if not exists (select * from syscolumns where name='JewelWeight' and id=OBJECT_ID(N'[dbo].[Inventory]'))
begin
alter table dbo.Inventory add JewelWeight decimal(24,2) not null default(0)
end

if not exists (select * from syscolumns where name='TotalWeight' and id=OBJECT_ID(N'[dbo].[Inventory]'))
begin
alter table dbo.Inventory add TotalWeight decimal(24,2) not null default(0)
end

if not exists (select * from syscolumns where name='QTY' and id=OBJECT_ID(N'[dbo].[Inventory]'))
begin
alter table dbo.Inventory add QTY decimal(24,2) not null default(0)
end

if not exists (select * from syscolumns where name='OrderNo' and id=OBJECT_ID(N'[dbo].[Inventory]'))
begin
alter table dbo.Inventory add OrderNo varchar(200) null
end

if not exists (select * from syscolumns where name='VendorSpec' and id=OBJECT_ID(N'[dbo].[Inventory]'))
begin
alter table dbo.Inventory add VendorSpec varchar(200) null
end

if not exists (select * from syscolumns where name='Length' and id=OBJECT_ID(N'[dbo].[Inventory]'))
begin
alter table dbo.Inventory add [Length] varchar(200) null
end
