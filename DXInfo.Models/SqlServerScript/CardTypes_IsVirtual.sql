if not exists (select * from syscolumns where name='IsVirtual' and id=OBJECT_ID(N'[dbo].[CardTypes]'))
begin
alter table dbo.CardTypes add IsVirtual bit not null default(0)
end
