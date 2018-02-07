IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbNotice]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbNotice](
	[id] [bigint] IDENTITY(1,1) NOT NULL primary key,
	[vcComments] [varchar](256) NULL,
	[dtCreateDate] [datetime] NULL,
	[vcActiveFlag] [char](1) NULL,
	[vcDeptFlag] [varchar](5) NULL,
)
end
