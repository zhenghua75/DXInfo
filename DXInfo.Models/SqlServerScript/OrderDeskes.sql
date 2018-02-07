IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderDeskes]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[OrderDeskes](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[OrderId] [uniqueidentifier] NOT NULL,
	[DeskId] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
)
end

