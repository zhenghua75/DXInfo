IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PlayLists]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[PlayLists](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[DeptId] [uniqueidentifier] NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[BeginTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
)
end
