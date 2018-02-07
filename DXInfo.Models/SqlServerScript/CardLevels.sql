IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardLevels]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[CardLevels](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[DeptId] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Discount] [decimal](24, 2) NOT NULL,
	[BeginLetter] [nvarchar](50) NULL,
	[Comment] [nvarchar](200) NULL,
)
end

if exists (select * from syscolumns where name='beginletter' and id=OBJECT_ID(N'[dbo].[cardlevels]') and isnullable=0)
begin
alter table cardlevels alter column beginletter nvarchar(50) null
end

if not exists (select * from syscolumns where name='Point' and id=OBJECT_ID(N'[dbo].[cardlevels]'))
begin
alter table dbo.cardlevels add [Point] [decimal](24, 2) NOT NULL default(0)
end

if not exists (select * from syscolumns where name='IsDefault' and id=OBJECT_ID(N'[dbo].[cardlevels]'))
begin
alter table dbo.cardlevels add [IsDefault] bit NOT NULL default(0)
end

