IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbLogin]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbLogin](
	[vcLoginID] [varchar](20) NOT NULL primary key,
	[vcOperName] [varchar](40) NOT NULL,
	[vcLimit] [varchar](5) NULL,
	[vcPwd] [varchar](20) NULL,
	[vcDeptID] [varchar](5) NULL,
) 
end


