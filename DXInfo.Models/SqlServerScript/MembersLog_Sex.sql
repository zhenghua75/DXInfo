if not exists(select * from syscolumns where name='Sex' and id=OBJECT_ID(N'[dbo].[MembersLog]'))
begin
alter table MembersLog add [Sex] [nvarchar](50) NULL
end