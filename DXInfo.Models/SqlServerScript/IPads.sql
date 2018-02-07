IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IPads]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[IPads](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[SN] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[Comment] [nvarchar](200) NULL,
)
end

