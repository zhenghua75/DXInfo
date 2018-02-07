IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InventoryCategory]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[InventoryCategory](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Comment] [nvarchar](50) NULL,
	[IsDiscount] [bit] NOT NULL DEFAULT ((1)),
	[CategoryType] [int] NOT NULL DEFAULT ((0)),
	[DeptType] [int] NOT NULL DEFAULT ((0)),
	[ProductType] [int] NOT NULL DEFAULT ((0)),
)
end

if exists (select * from syscolumns where name='DeptType' and id=OBJECT_ID(N'[dbo].[InventoryCategory]') and isnullable=0)
begin
alter table InventoryCategory alter column [DeptType] int null
end

if exists (select * from syscolumns where name='ProductType' and id=OBJECT_ID(N'[dbo].[InventoryCategory]') and isnullable=0)
begin
alter table InventoryCategory alter column [ProductType] int null
end

if exists (select * from syscolumns where name='DeptType' and id=OBJECT_ID(N'[dbo].[InventoryCategory]'))
begin
EXEC sp_rename 'InventoryCategory.DeptType', 'SectionType', 'COLUMN'
end