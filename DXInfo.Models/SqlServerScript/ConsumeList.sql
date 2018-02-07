IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ConsumeList]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[ConsumeList](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Consume] [uniqueidentifier] NOT NULL,
	[Inventory] [uniqueidentifier] NOT NULL,
	[Cup] [int] NOT NULL,
	[Price] [decimal](24, 2) NOT NULL,
	[Quantity] [decimal](24, 2) NOT NULL,
	[Amount] [decimal](24, 2) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[DeptId] [uniqueidentifier] NOT NULL,
	[Sum] [decimal](24, 2) NOT NULL DEFAULT ((0)),
	[Discount] [decimal](24, 2) NOT NULL DEFAULT ((0)),
	[IsPackage] [bit] NOT NULL DEFAULT ((0)),
	[PackageId] [uniqueidentifier] NULL,
)
end

if not exists (select * from syscolumns where name='IsValid' and id=OBJECT_ID(N'[dbo].[ConsumeList]'))
begin
alter table dbo.ConsumeList add IsValid bit not null default(1)
end

if not exists (select * from syscolumns where name='IsStock' and id=OBJECT_ID(N'[dbo].[ConsumeList]'))
begin
alter table dbo.ConsumeList add IsStock bit not null default(0)
end

if not exists (select * from syscolumns where name='AgreementPrice' and id=OBJECT_ID(N'[dbo].[ConsumeList]'))
begin
alter table dbo.ConsumeList add AgreementPrice decimal(24,2) null
end