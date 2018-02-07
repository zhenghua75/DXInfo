IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UnitOfMeasures]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[UnitOfMeasures](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Comment] [nvarchar](50) NULL,
	[Group] [uniqueidentifier] NOT NULL DEFAULT ('00000000-0000-0000-0000-000000000000'),
	[Rate] [numeric](24, 9) NOT NULL DEFAULT ((0)),
	[IsMain] [bit] NOT NULL DEFAULT ((0)),
	[UOMType] [int] NOT NULL DEFAULT ((0)),
)
end

