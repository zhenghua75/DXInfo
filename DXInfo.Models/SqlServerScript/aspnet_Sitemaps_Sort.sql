if not exists (select * from syscolumns where name='Sort' and id=OBJECT_ID(N'[dbo].[aspnet_Sitemaps]') and isnullable=0)
begin
alter table dbo.aspnet_Sitemaps add Sort int not null default(0)
end