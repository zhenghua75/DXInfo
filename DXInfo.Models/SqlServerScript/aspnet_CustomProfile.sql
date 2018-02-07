IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_CustomProfile]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[aspnet_CustomProfile](
	[UserId] [uniqueidentifier] NOT NULL primary key,
	[DeptId] [uniqueidentifier] NULL,
	[FullName] [nvarchar](256) NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
)
end
