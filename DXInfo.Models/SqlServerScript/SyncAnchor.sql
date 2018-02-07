IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SyncAnchor]') AND type in (N'U'))
begin
create table [dbo].[SyncAnchor]
(
	Id int not null primary key,
	Anchor binary(8) not null,
)
end