IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderBookDeskesHis]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[OrderBookDeskesHis](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[LinkId] [uniqueidentifier] NOT NULL,
	[OrderBookId] [uniqueidentifier] NOT NULL,
	[DeskId] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
)
end

