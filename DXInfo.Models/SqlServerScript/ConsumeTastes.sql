IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ConsumeTastes]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[ConsumeTastes](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[ConsumeList] [uniqueidentifier] NOT NULL,
	[Taste] [uniqueidentifier] NOT NULL,
)
end

