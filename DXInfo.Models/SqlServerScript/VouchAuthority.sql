IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VouchAuthority]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[VouchAuthority](
	[UserId] [uniqueidentifier] NOT NULL primary key,
	[AuthorityType] [int] NOT NULL DEFAULT ((2)),
)
end

