IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Recharges]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Recharges](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Card] [uniqueidentifier] NOT NULL,
	[PayType] [uniqueidentifier] NOT NULL,
	[LastBalance] [decimal](24, 2) NOT NULL,
	[Amount] [decimal](24, 2) NOT NULL,
	[Donate] [decimal](24, 2) NOT NULL,
	[Balance] [decimal](24, 2) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[DeptId] [uniqueidentifier] NOT NULL,
	[RechargeType] [int] NOT NULL,
)
end

if not exists (select * from syscolumns where name='OperatorsOnDuty' and id=OBJECT_ID(N'[dbo].[Recharges]'))
begin
alter table dbo.Recharges add OperatorsOnDuty varchar(200) null
end