IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RdRecord]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[RdRecord](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Code] [varchar](50) NOT NULL,
	[RdFlag] [int] NOT NULL,
	[RdCode] [varchar](50) NOT NULL,
	[VouchType] [varchar](50) NOT NULL,
	[BusType] [varchar](50) NOT NULL,
	[PTCode] [nvarchar](200) NULL,
	[STCode] [nvarchar](200) NULL,
	[RdDate] [date] NOT NULL,
	[WhId] [uniqueidentifier] NOT NULL,
	[ARVCode] [varchar](50) NULL,
	[VenId] [uniqueidentifier] NULL,
	[DeptId] [uniqueidentifier] NOT NULL,
	[ArvDate] [date] NULL,
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
	[RedVouch] [bit] NOT NULL DEFAULT ((0)),
	[PUInit] [bit] NOT NULL DEFAULT ((0)),
	[InvInit] [bit] NOT NULL DEFAULT ((0)),
	[STInit] [bit] NOT NULL DEFAULT ((0)),
	[Salesman] [uniqueidentifier] NOT NULL,
	[SourceId] [uniqueidentifier] NULL,
	[SourceCode] [varchar](50) NULL,
)
end


