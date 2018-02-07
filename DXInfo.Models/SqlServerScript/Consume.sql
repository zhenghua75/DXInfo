IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Consume]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Consume](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Card] [uniqueidentifier] NULL,
	[PayType] [uniqueidentifier] NULL,
	[LastBalance] [decimal](24, 2) NOT NULL,
	[Sum] [decimal](24, 2) NOT NULL,
	[PayVoucher] [decimal](24, 2) NOT NULL,
	[Voucher] [decimal](24, 2) NOT NULL,
	[Discount] [decimal](24, 2) NOT NULL,
	[Amount] [decimal](24, 2) NOT NULL,
	[Point] [decimal](24, 2) NOT NULL,
	[Balance] [decimal](24, 2) NOT NULL,
	[Cash] [decimal](24, 2) NOT NULL,
	[Change] [decimal](24, 2) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[DeptId] [uniqueidentifier] NOT NULL,
	[ConsumeType] [int] NOT NULL,
	[DeskNo] [nvarchar](50) NULL,
	[OrderId] [uniqueidentifier] NULL,
	[Quantity] [decimal](24, 2) NOT NULL DEFAULT ((0)),
	[SourceType] [int] NOT NULL DEFAULT ((0)),
)
end

if not exists (select * from syscolumns where name='IsValid' and id=OBJECT_ID(N'[dbo].[Consume]'))
begin
alter table dbo.Consume add IsValid bit not null default(1)
end

if not exists (select * from syscolumns where name='Member' and id=OBJECT_ID(N'[dbo].[Consume]'))
begin
alter table dbo.Consume add Member uniqueidentifier null
end

if not exists (select * from syscolumns where name='Sn' and id=OBJECT_ID(N'[dbo].[Consume]'))
begin
alter table dbo.Consume add Sn varchar(200) null
end

if not exists (select * from syscolumns where name='OperatorsOnDuty' and id=OBJECT_ID(N'[dbo].[Consume]'))
begin
alter table dbo.Consume add OperatorsOnDuty varchar(200) null
end