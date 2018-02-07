IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderBooks]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[OrderBooks](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[BookBeginDate] [datetime] NOT NULL,
	[BookEndDate] [datetime] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Customer] [nvarchar](50) NOT NULL,
	[LinkPhone] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[DeptId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[Status] [int] NOT NULL,
	[Comment] [nvarchar](200) NULL,
)
end

