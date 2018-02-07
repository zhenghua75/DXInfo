IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transports]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Transports](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Card] [uniqueidentifier] NOT NULL,
	[InFactory_Date] [datetime] NOT NULL,
	[InFactory_UserId] [uniqueidentifier] NOT NULL,
	[InFactory_DeptId] [uniqueidentifier] NOT NULL,
	[Load_Date] [datetime] NULL,
	[Load_UserId] [uniqueidentifier] NULL,
	[Load_DeptId] [uniqueidentifier] NULL,
	[Load_Inventory] [uniqueidentifier] NULL,
	[Load_Quantity] [numeric](18, 3) NULL,
	[Shipment_Date] [datetime] NULL,
	[Shipment_UserId] [uniqueidentifier] NULL,
	[Shipment_DeptId] [uniqueidentifier] NULL,
	[Shipment_Quantity] [numeric](18, 3) NULL,
	[Shipment_CheckUser] [nvarchar](200) NULL,
	[OutFactory_Date] [datetime] NULL,
	[OutFactory_UserId] [uniqueidentifier] NULL,
	[OutFactory_DeptId] [uniqueidentifier] NULL,
	[BalanceType] [uniqueidentifier] NULL,
	[AgreeFreightRate] [numeric](18, 3) NULL,
	[FreightRate] [numeric](18, 3) NULL,
	[Comment] [nvarchar](50) NULL,
	[Shipper] [nvarchar](50) NULL,
	[Shipper_Telephone] [nvarchar](50) NULL,
	[Carrier] [nvarchar](50) NULL,
	[Carrier_Telephone] [nvarchar](50) NULL,
	[Lines] [uniqueidentifier] NULL,
	[Driver] [uniqueidentifier] NULL,
	[Status] [int] NOT NULL,
	[ModifyUserId] [uniqueidentifier] NULL,
	[ModifyDeptId] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
)
end

