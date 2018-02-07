IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbFunc]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbFunc](
	[cnvcFuncName] [varchar](30) NOT NULL primary key,
	[cnvcFuncParentName] [varchar](30) NOT NULL,
	[cnvcFuncAddress] [varchar](200) NULL,
	[cnvcFuncType] [varchar](20) NOT NULL,
) 
end