IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BillDonateInvLists]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[BillDonateInvLists](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Bill] [uniqueidentifier] NOT NULL,
	[InvName] [nvarchar](50) NULL,
)
end

