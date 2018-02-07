IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ConsumeDonateInv]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[ConsumeDonateInv](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Consume] [uniqueidentifier] NOT NULL,
	[Inventory] [uniqueidentifier] NOT NULL,
)
end
