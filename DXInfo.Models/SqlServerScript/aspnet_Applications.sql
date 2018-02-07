IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Applications]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[aspnet_Applications](
	[ApplicationName] [nvarchar](256) NOT NULL UNIQUE,
	[LoweredApplicationName] [nvarchar](256) NOT NULL UNIQUE,
	[ApplicationId] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Description] [nvarchar](256) NULL,
)
end