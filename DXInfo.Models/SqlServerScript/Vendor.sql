IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Vendor]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Vendor](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Code] [varchar](50) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Tel] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Zip] [varchar](50) NULL,
	[Linkman] [nvarchar](200) NULL,
	[Address] [nvarchar](200) NULL,
	[Email] [varchar](50) NULL,
)
end

if not exists (select * from syscolumns where name='VendorType' and id=OBJECT_ID(N'[dbo].[Vendor]'))
begin
alter table dbo.Vendor add VendorType int not null default(0)
end