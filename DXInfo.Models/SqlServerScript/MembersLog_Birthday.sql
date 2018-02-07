if not exists(select * from syscolumns where name='Birthday' and id=OBJECT_ID(N'[dbo].[MembersLog]'))
begin
alter table MembersLog add [Birthday] [datetime] NULL
end