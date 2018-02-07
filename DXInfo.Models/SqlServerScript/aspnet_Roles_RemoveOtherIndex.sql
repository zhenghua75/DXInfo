if exists (select * from sysindexes where id=object_id('aspnet_Roles') and indid>0 and name not like '_WA_Sys_%' and name not like 'PK_%')
begin
if object_id('tempdb..#zhh_t_aspnet_Roles') is not null
   drop table #zhh_t_aspnet_Roles
select name into #zhh_t_aspnet_Roles from sysindexes where id=object_id('aspnet_Roles') and indid>0 and name not like '_WA_Sys_%' and name not like 'PK_%'
declare @aspnet_Roles_name varchar(30)
declare cur cursor for select name from #zhh_t_aspnet_Roles
open cur
fetch next from cur into @aspnet_Roles_name
while (@@fetch_status=0)
begin
exec('drop index aspnet_Roles.['+@aspnet_Roles_name+']')
fetch next from cur into @aspnet_Roles_name 
end
close cur
deallocate cur
drop table #zhh_t_aspnet_Roles 
end