IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderInvPrice]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[OrderInvPrice](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[InvId] [uniqueidentifier] NOT NULL,
	[SalePrice] [decimal](24, 2) NOT NULL,
	[SalePoint] [decimal](24, 2) NOT NULL,
	[InvPriceId] [uniqueidentifier] NOT NULL,
	[OrderMenuId] [uniqueidentifier] NOT NULL,
)
end
