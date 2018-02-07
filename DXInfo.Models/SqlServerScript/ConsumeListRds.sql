IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ConsumeListRds]') AND type in (N'U'))
begin
CREATE TABLE [dbo].ConsumeListRds(
	[ConsumeListId] [uniqueidentifier] NOT NULL primary key,
	[RdsId] [uniqueidentifier] NOT NULL,
) 
end