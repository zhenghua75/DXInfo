IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ConsumePackages]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[ConsumePackages](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Consume] [uniqueidentifier] NOT NULL,
	[Inventory] [uniqueidentifier] NOT NULL,
	[Price] [decimal](24, 2) NOT NULL,
	[Quantity] [decimal](24, 2) NOT NULL,
	[Amount] [decimal](24, 2) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[DeptId] [uniqueidentifier] NOT NULL,
	[Sum] [decimal](24, 2) NOT NULL DEFAULT ((0)),
	[Discount] [decimal](24, 2) NOT NULL DEFAULT ((0)),
)
end
