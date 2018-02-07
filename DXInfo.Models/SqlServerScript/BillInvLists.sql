IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BillInvLists]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[BillInvLists](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Bill] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CupType] [nvarchar](50) NULL,
	[Tastes] [nvarchar](200) NULL,
	[SalePrice] [decimal](24, 2) NULL,
	[Quantity] [decimal](24, 2) NULL,
	[Amount] [decimal](24, 2) NULL,
)
end

if not exists (select * from syscolumns where name='Discount' and id=OBJECT_ID(N'[dbo].[BillInvLists]'))
begin
alter table dbo.BillInvLists add Discount decimal(24,2) null
end

if not exists (select * from syscolumns where name='Sum' and id=OBJECT_ID(N'[dbo].[BillInvLists]'))
begin
alter table dbo.BillInvLists add [Sum] decimal(24,2) null
end

if not exists (select * from syscolumns where name='AgreementPrice' and id=OBJECT_ID(N'[dbo].[BillInvLists]'))
begin
alter table dbo.BillInvLists add AgreementPrice decimal(24,2) null
end