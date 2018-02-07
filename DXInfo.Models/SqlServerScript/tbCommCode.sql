IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbCommCode]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbCommCode](
	[Id] int identity(1,1) not null primary key, 
	[vcCommName] [varchar](20) NOT NULL,
	[vcCommCode] [varchar](5) NOT NULL,
	[vcCommSign] [varchar](5) NOT NULL,
	[vcComments] [varchar](20) NULL,
) 
end


if not exists (select * from syscolumns where name='Id' and id=OBJECT_ID(N'[dbo].[tbCommCode]'))
begin
alter table dbo.tbCommCode add Id int identity(1,1) not null
ALTER TABLE [dbo].[tbCommCode] DROP CONSTRAINT [PK_TBCOMMCODE]
ALTER TABLE [dbo].[tbCommCode] ADD  CONSTRAINT [PK_TBCOMMCODE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
end

if exists (select * from syscolumns where name='vcCommSign' and id=OBJECT_ID(N'[dbo].[tbCommCode]') and length=5)
begin
alter table dbo.tbCommCode alter column vcCommSign varchar(20) not null
end