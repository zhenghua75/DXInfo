IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[vw_UpdateInfoData]') AND type in (N'V'))
begin
exec('create view dbo.vw_UpdateInfoData
as
select 
SUM(a.BillQuantity) BillQuantity,
SUM(a.MenuQuantity) MenuQuantity,
SUM(a.MissQuantity) MissQuantity,
SUM(a.Quantity) Quantity from OrderMenus a
left join OrderDishes b on a.OrderId=b.Id
where (b.Status=0 or b.Status=3)
and a.Status>0 and a.Status!=8')
end