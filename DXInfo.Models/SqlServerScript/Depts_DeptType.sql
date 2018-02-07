if not exists(select * from syscolumns where name='DeptType' and id=OBJECT_ID(N'[dbo].[Depts]'))
begin
alter table Depts add DeptType int not null default(0)
end