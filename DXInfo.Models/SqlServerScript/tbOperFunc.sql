IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbOperFunc]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbOperFunc](
	[vcOperID] [varchar](10) NOT NULL,
	[vcFuncName] [varchar](30) NOT NULL,
	[vcFuncAddress] [varchar](200) NULL,
 CONSTRAINT [PK_TBOPERFUNC] PRIMARY KEY CLUSTERED 
(
	[vcOperID] ASC,
	[vcFuncName] ASC
)
) 
end
