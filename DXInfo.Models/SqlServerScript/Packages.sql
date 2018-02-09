IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Packages]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Packages](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[PackageId] [uniqueidentifier] NOT NULL,
	[InventoryId] [uniqueidentifier] NOT NULL,
	[Price] [decimal](24, 2) NOT NULL,
	[IsOptional] [bit] NOT NULL,
	[OptionalGroup] [nvarchar](200) NULL,
	[Comment] [nvarchar](200) NULL,
)
end


if not exists (select * from syscolumns where name='Quantity' and id=OBJECT_ID(N'[dbo].[Packages]'))
begin
alter table dbo.Packages add Quantity int not null default(0)
end