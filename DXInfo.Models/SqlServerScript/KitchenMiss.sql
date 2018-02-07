IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KitchenMiss]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[KitchenMiss](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[OrderMenuId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NULL,
	[Quantity] [decimal](24, 2) NULL DEFAULT ((0)),
)
end

