IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RechargeDonations]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[RechargeDonations](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[DeptId] [uniqueidentifier] NOT NULL,
	[BeginAmount] [decimal](24, 2) NOT NULL,
	[DonationRatio] [decimal](24, 2) NOT NULL,
	[DonationTopLimit] [decimal](24, 2) NOT NULL,
	[Comment] [nvarchar](200) NULL,
)
end


