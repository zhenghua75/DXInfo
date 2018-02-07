IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderHurry]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[OrderHurry](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[OrderId] [uniqueidentifier] NOT NULL,
	[InventoryId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NOT NULL,
)
end

