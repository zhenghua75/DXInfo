IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderSequences]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[OrderSequences](
	[Id] [int] IDENTITY(1,1) NOT NULL primary key,
	[Fill] [char](1) NOT NULL DEFAULT ('0'),
)
end

