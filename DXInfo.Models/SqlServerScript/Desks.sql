IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Desks]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Desks](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[RoomId] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Size] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Comment] [nvarchar](200) NULL,
)
end
