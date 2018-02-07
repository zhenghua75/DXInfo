IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardsLog]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[CardsLog](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[CardId] [uniqueidentifier] NOT NULL,
	[CardNo] [nvarchar](50) NOT NULL,
	[SecondCardNo] [nvarchar](50) NULL,
	[Member] [uniqueidentifier] NOT NULL,
	[CardType] [uniqueidentifier] NOT NULL,
	[CardLevel] [uniqueidentifier] NOT NULL,
	[Balance] [decimal](24, 2) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LossUserId] [uniqueidentifier] NOT NULL,
	[LossDate] [datetime] NULL,
	[LossDeptId] [uniqueidentifier] NULL,
	[FoundUserId] [uniqueidentifier] NOT NULL,
	[FoundDate] [datetime] NULL,
	[FoundDeptId] [uniqueidentifier] NULL,
	[AddDate] [datetime] NULL,
	[AddUserId] [uniqueidentifier] NULL,
	[AddDeptId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[DeptId] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[Comment] [nvarchar](50) NULL,
	[AddReason] [nvarchar](50) NULL,
	[CardPwd] [nvarchar](50) NULL,
)
end