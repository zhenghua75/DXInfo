IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderMenusHis]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[OrderMenusHis](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[LinkId] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[InventoryId] [uniqueidentifier] NOT NULL,
	[Price] [decimal](24, 2) NOT NULL,
	[Quantity] [decimal](24, 2) NOT NULL,
	[Amount] [decimal](24, 2) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Comment] [nvarchar](200) NULL,
	[Status] [int] NOT NULL DEFAULT ((0)),
	[BackUserId] [uniqueidentifier] NULL,
	[BackCreateDate] [datetime] NULL,
	[BackQuantity] [decimal](24, 2) NOT NULL DEFAULT ((0)),
	[BackReseaon] [nvarchar](200) NULL,
	[OrderUserId] [uniqueidentifier] NULL,
	[OrderCreateDate] [datetime] NULL,
	[MissUserId] [uniqueidentifier] NULL,
	[MissCreateDate] [datetime] NULL,
	[HurryUserId] [uniqueidentifier] NULL,
	[HurryCreateDate] [datetime] NULL,
	[BillUserId] [uniqueidentifier] NULL,
	[BillCreateDate] [datetime] NULL,
	[MenuUserId] [uniqueidentifier] NULL,
	[MenuCreateDate] [datetime] NULL,
	[MenuQuantity] [decimal](24, 2) NOT NULL DEFAULT ((0)),
	[Oper] [uniqueidentifier] NULL,
	[OperDate] [datetime] NULL,
	[BillQuantity] [decimal](24, 2) NOT NULL DEFAULT ((0)),
	[MissQuantity] [decimal](24, 2) NOT NULL DEFAULT ((0)),
	[IsPackage] [bit] NOT NULL DEFAULT ((0)),
	[PackageId] [uniqueidentifier] NULL,
)
end


