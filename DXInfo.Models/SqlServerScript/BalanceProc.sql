IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BalanceProc]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[BalanceProc](
	[LocalDeptId] [uniqueidentifier] NOT NULL,
	[LocalDeptName] [nvarchar](256) NOT NULL,
	[RemoteDeptId] [uniqueidentifier] NOT NULL,
	[RemoteDeptName] [nvarchar](256) NOT NULL,
	[BalanceDate] [date] NOT NULL,
	[FillFee] [numeric](24, 2) NOT NULL,
	[Fee] [numeric](24, 2) NOT NULL,
	[FillProm] [numeric](24, 2) NOT NULL,
 CONSTRAINT [PK_BALANCEPROC] PRIMARY KEY CLUSTERED 
(
	[LocalDeptId] ASC,
	[RemoteDeptId] ASC,
	[BalanceDate] ASC
)
) 
end