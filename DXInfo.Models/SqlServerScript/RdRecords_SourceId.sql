if not exists (select * from syscolumns where name='SourceId' and id=OBJECT_ID(N'[dbo].[RdRecords]'))
begin
alter table dbo.RdRecords add SourceId uniqueidentifier null
end
