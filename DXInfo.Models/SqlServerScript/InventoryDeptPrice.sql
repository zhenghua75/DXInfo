IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InventoryDeptPrice]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[InventoryDeptPrice](
	[DeptId] [uniqueidentifier] NOT NULL,
	[InvId] [uniqueidentifier] NOT NULL,
	[SalePrice] [decimal](24, 2) NOT NULL,
	[SalePrice0] [decimal](24, 2) NOT NULL,
	[SalePrice1] [decimal](24, 2) NOT NULL,
	[SalePrice2] [decimal](24, 2) NOT NULL,
	[SalePoint] [decimal](24, 2) NOT NULL,
	[SalePoint0] [decimal](24, 2) NOT NULL,
	[SalePoint1] [decimal](24, 2) NOT NULL,
	[SalePoint2] [decimal](24, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DeptId] ASC,
	[InvId] ASC
)
)
end

