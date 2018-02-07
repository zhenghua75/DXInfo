IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbOper]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbOper](
	[vcOperID] [varchar](10) NOT NULL primary key,
	[vcOperName] [varchar](10) NOT NULL,
	[vcLimit] [varchar](5) NULL,
	[vcPwd] [varchar](6) NULL,
	[vcDeptID] [varchar](5) NULL,
	[vcActiveFlag] [varchar](5) NULL,
	[vcPwdBeginFlag] [nvarchar](5) NULL,
)
end

if not exists (select * from syscolumns where name='IsDiscount' and id=OBJECT_ID(N'[dbo].[tbOper]'))
begin
alter table dbo.tbOper add IsDiscount bit null
end