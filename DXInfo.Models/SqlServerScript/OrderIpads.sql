IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderIpads]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[OrderIpads](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[OrderId] [uniqueidentifier] NOT NULL,
	[IPadId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
)
end