IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Sitemaps]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[aspnet_Sitemaps](
	[Code] [nvarchar](256) NOT NULL primary key,
	[Name] [nvarchar](256) NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NOT NULL,
	[Controller] [nvarchar](256) NULL,
	[Action] [nvarchar](256) NULL,
	[ParaId] [nvarchar](256) NULL,
	[Url] [nvarchar](256) NULL,
	[ParentCode] [nvarchar](256) NOT NULL,
	[IsAuthorize] [bit] NOT NULL,
	[IsMenu] [bit] NOT NULL,
	[IsClient] [bit] NOT NULL DEFAULT ((0)),
)
end