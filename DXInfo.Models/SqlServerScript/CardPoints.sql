IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardPoints]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[CardPoints](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Card] [uniqueidentifier] NOT NULL,
	[Point] [decimal](24, 2) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[DeptId] [uniqueidentifier] NOT NULL,
	[PointType] [int] NOT NULL,
)
end
