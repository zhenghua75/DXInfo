IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[scope_config]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[scope_config](
	[config_id] [uniqueidentifier] NOT NULL primary key,
	[config_data] [xml] NOT NULL,
	[scope_status] [char](1) NULL,
)
end

