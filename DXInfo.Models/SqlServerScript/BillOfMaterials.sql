IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BillOfMaterials]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[BillOfMaterials](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[PartInvId] [uniqueidentifier] NOT NULL,
	[ComponentInvId] [uniqueidentifier] NOT NULL,
	[BaseQtyN] [decimal](24, 9) NOT NULL,
	[BaseQtyD] [decimal](24, 9) NOT NULL,
)
end