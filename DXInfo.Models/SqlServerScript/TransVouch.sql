IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TransVouch]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[TransVouch](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Code] [varchar](50) NOT NULL,
	[OutRdCode] [varchar](50) NOT NULL,
	[InRdCode] [varchar](50) NOT NULL,
	[TVDate] [date] NOT NULL,
	[OutWhId] [uniqueidentifier] NOT NULL,
	[InWhId] [uniqueidentifier] NOT NULL,
	[OutDeptId] [uniqueidentifier] NOT NULL,
	[InDeptId] [uniqueidentifier] NOT NULL,
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
	[Salesman] [uniqueidentifier] NULL,
	[TransFlag] [bit] NOT NULL DEFAULT ((0)),
	[SourceId] [uniqueidentifier] NULL,
)
end

