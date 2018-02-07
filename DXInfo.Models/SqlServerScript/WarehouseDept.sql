IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WarehouseDept]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[WarehouseDept](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Dept] [uniqueidentifier] NOT NULL,
	[Warehouse] [uniqueidentifier] NOT NULL,
)
end

