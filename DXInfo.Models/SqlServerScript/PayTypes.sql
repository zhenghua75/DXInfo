IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayTypes]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[PayTypes](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Comment] [nvarchar](200) NULL,
)
end

if not exists (select * from syscolumns where name='PayType' and id=OBJECT_ID(N'[dbo].[PayTypes]'))
begin
alter table dbo.PayTypes add PayType int not null default(0)
end