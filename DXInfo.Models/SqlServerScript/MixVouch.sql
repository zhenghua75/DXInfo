IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MixVouch]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[MixVouch](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Code] [varchar](50) NOT NULL,
	[MVDate] [date] NOT NULL,
	[OutDeptId] [uniqueidentifier] NOT NULL,
	[OutWhId] [uniqueidentifier] NOT NULL,
	[InDeptId] [uniqueidentifier] NOT NULL,
	[InWhId] [uniqueidentifier] NOT NULL,
	[Salesman] [uniqueidentifier] NOT NULL,
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
)
end

