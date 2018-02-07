if not exists (select * from syscolumns where name='CardNoRule' and id=OBJECT_ID(N'[dbo].[CardTypes]'))
begin
alter table dbo.CardTypes add CardNoRule varchar(200) null
end