IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InvDepts]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[InvDepts](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Inv] [uniqueidentifier] NOT NULL,
	[Dept] [uniqueidentifier] NOT NULL,
	[Comment] [nvarchar](200) NULL,
)
end

