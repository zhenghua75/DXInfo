IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lines]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Lines](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Mileage] [numeric](18, 3) NULL,
	[Comment] [ntext] NULL,
)
end

