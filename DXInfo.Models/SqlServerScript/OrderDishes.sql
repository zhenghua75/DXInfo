IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderDishes]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[OrderDishes](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[OrderNo] [int] NOT NULL,
	[OrderPwd] [nvarchar](50) NULL,
	[Quantity] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[DeptId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[IsIpad] [bit] NOT NULL,
	[Status] [int] NOT NULL,
	[Comment] [nvarchar](200) NULL,
)
end
