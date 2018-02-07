IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MonthBalance]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[MonthBalance](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[MBDate] [date] NOT NULL,
	[Period] [uniqueidentifier] NOT NULL,
	[DeptId] [uniqueidentifier] NOT NULL,
	[WhId] [uniqueidentifier] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[Maker] [uniqueidentifier] NOT NULL,
	[MakeDate] [date] NOT NULL,
	[MakeTime] [datetime] NOT NULL,
	[IsVerify] [bit] NOT NULL DEFAULT ((0)),
	[Verifier] [uniqueidentifier] NULL,
	[VerifyDate] [date] NULL,
	[VerifyTime] [datetime] NULL,
	[Modifier] [uniqueidentifier] NULL,
	[ModifyDate] [date] NULL,
	[ModifyTime] [datetime] NULL,
	[Salesman] [uniqueidentifier] NOT NULL,
)
end

