IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ekey]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[ekey](
	[HardwareID] [varchar](100) NOT NULL primary key,
	[CardNo] [varchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsUse] [bit] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
)
end