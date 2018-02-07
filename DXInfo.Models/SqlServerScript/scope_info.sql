IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[scope_info]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[scope_info](
	[scope_local_id] [int] IDENTITY(1,1) NOT NULL,
	[scope_id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) ,
	[sync_scope_name] [nvarchar](100) NOT NULL primary key,
	[scope_sync_knowledge] [varbinary](max) NULL,
	[scope_tombstone_cleanup_knowledge] [varbinary](max) NULL,
	[scope_timestamp] [timestamp] NULL,
	[scope_config_id] [uniqueidentifier] NULL,
	[scope_restore_count] [int] NOT NULL DEFAULT ((0)),
	[scope_user_comment] [nvarchar](max) NULL,
)
end

