IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuStatus]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[MenuStatus](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Dept] [uniqueidentifier] NOT NULL,
	[Inventory] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
)
end

