IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_AuthorizationRules]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[aspnet_AuthorizationRules](
	[RuleId] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[SiteMapKey] [nvarchar](256) NOT NULL,
	[RoleId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
) 
end