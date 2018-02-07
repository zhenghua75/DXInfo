IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DXInfo_UpdateOrderMenuDataComplete]') AND type in (N'P'))
begin
drop procedure sp_DXInfo_UpdateOrderMenuDataComplete
end
IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DXInfo_UpdateOrderMenuDataComplete]') AND type in (N'P'))
begin
exec('
create procedure sp_DXInfo_UpdateOrderMenuDataComplete
   @SectionType int
as
begin
select a.OrderCreateDate as CreateDate,a.Id OrderMenuId,a.OrderId,c.Name as InvName,
a.InventoryId,a.Comment,a.Quantity,a.BillQuantity,a.MissQuantity,a.MenuQuantity,a.Price,
e.FullName,a.[Status],c.Printer,
case when a.MenuCreateDate is null then
case when a.OrderCreateDate is null then 0 else DateDiff(minute,a.OrderCreateDate,GETDATE()) end
else DateDiff(minute,a.OrderCreateDate,a.MenuCreateDate) end WaitMinutes
 from OrderMenus a
left join OrderDishes b on a.OrderId=b.Id
left join Inventory c on a.InventoryId=c.Id
left join InventoryCategory d on c.Category=d.Id
left join aspnet_CustomProfile e on a.UserId=e.UserId
where 
(b.Status=0 or b.Status=3)
and (a.Quantity<=a.MenuQuantity+a.MissQuantity or a.Status=1 or a.Status=7)
and not (a.Status=1 or a.Status=7)
and d.SectionType=@SectionType
and a.Status>0 and a.Status!=8
order by a.OperDate desc
end')
end