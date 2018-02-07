IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WarehouseInventory]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[WarehouseInventory](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Inventory] [uniqueidentifier] NOT NULL,
	[Warehouse] [uniqueidentifier] NOT NULL,
	[Quantity] [decimal](24, 9) NOT NULL DEFAULT ((0)),
)
end

