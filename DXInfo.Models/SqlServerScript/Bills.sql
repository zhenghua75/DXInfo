IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bills]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Bills](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[BillType] [nvarchar](50) NOT NULL,
	[CardNo] [nvarchar](50) NULL,
	[MemberName] [nvarchar](200) NULL,
	[LastBalance] [decimal](24, 2) NULL,
	[Sum] [decimal](24, 2) NULL,
	[Voucher] [decimal](24, 2) NULL,
	[Discount] [decimal](24, 2) NULL,
	[Amount] [decimal](24, 2) NULL,
	[Donate] [decimal](24, 2) NULL,
	[Balance] [decimal](24, 2) NULL,
	[Cash] [decimal](24, 2) NOT NULL,
	[Change] [decimal](24, 2) NOT NULL,
	[FullName] [nvarchar](256) NULL,
	[DeptName] [nvarchar](256) NULL,
	[CreateDate] [datetime] NULL,
	[PayTypeName] [nvarchar](50) NULL,
	[DeskNo] [nvarchar](50) NULL,
)
end

if not exists (select * from syscolumns where name='Sn' and id=OBJECT_ID(N'[dbo].[Bills]'))
begin
alter table dbo.Bills add Sn varchar(200) null
end

if not exists (select * from syscolumns where name='PeopleCount' and id=OBJECT_ID(N'[dbo].[Bills]'))
begin
alter table dbo.Bills add PeopleCount int null
end