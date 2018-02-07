IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WRCardLevels]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[WRCardLevels](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[CardLevel] [uniqueidentifier] NULL,
	[Discount] [decimal](24, 2) NOT NULL,
	[Comment] [nvarchar](200) NULL,
)
end
