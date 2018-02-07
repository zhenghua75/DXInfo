IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[vw_MenuDeskInfo]') AND type in (N'V'))
begin
exec('create view dbo.vw_MenuDeskInfo
as
select a.OrderId,a.Id OrderDeskId,a.DeskId,b.Code DeskCode from OrderDeskes a
left join Desks b on a.DeskId=b.Id
left join OrderDishes c on a.OrderId=c.Id
where (c.Status=0 or c.Status=3)
and a.Status=0
and b.Status=1')
end