IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardDonateInventory]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[CardDonateInventory](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[CardId] [uniqueidentifier] NOT NULL,
	[Inventory] [uniqueidentifier] NOT NULL,
	[IsValidate] [bit] NOT NULL DEFAULT ((0)),
	[InvalideDate] [datetime] NOT NULL,
)
end