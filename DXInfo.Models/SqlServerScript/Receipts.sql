IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Receipts]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Receipts](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	Member uniqueidentifier not null,
	Sn varchar(200) null,
	Content nvarchar(2000) null,
	[Status] int not null default(0),
	ReceiptType int not null default(0),		
	Comment nvarchar(200) null,
	[UserId] [uniqueidentifier] NOT NULL,
	[DeptId] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifyUserId] [uniqueidentifier] NULL,
	[ModifyDeptId] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
)
end

IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReceiptHis]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[ReceiptHis](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	LinkId uniqueidentifier not null,
	Member uniqueidentifier not null,
	Sn varchar(200) null,
	Content nvarchar(2000) null,
	[Status] int not null default(0),	
	ReceiptType int not null default(0),	
	Comment nvarchar(200) null,
	[UserId] [uniqueidentifier] NOT NULL,
	[DeptId] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifyUserId] [uniqueidentifier] NULL,
	[ModifyDeptId] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
)
end