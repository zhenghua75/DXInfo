﻿IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DXInfo_UpdateOrderMenuData]') AND type in (N'P'))
begin
drop procedure sp_DXInfo_UpdateOrderMenuData
end
IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DXInfo_UpdateOrderMenuData]') AND type in (N'P'))
begin
exec('
create procedure sp_DXInfo_UpdateOrderMenuData
   @SectionType int
as
begin
select a.OrderCreateDate as CreateDate,a.Id OrderMenuId,a.OrderId,c.Name as InvName,
a.InventoryId,a.Comment,a.Quantity,a.BillQuantity,a.MissQuantity,a.MenuQuantity,a.Price,
e.FullName,a.[Status],
case when a.[Status] = 4 then 0 
when a.[Status] = 5 then 1
when a.[Status] = 6 then 2
when a.[Status] = 2 then 3
when a.[Status] = 3 then 4 else 5 end Sort,c.Printer,
case when a.OrderCreateDate is null then 0 else DateDiff(minute,a.OrderCreateDate,GETDATE()) end WaitMinutes
 from OrderMenus a
left join OrderDishes b on a.OrderId=b.Id
left join Inventory c on a.InventoryId=c.Id
left join InventoryCategory d on c.Category=d.Id
left join aspnet_CustomProfile e on a.UserId=e.UserId
where 
(b.Status=0 or b.Status=3)
and a.Quantity>a.MenuQuantity+a.MissQuantity
and not (a.Status=1 or a.Status=7)
and d.SectionType=@SectionType
and a.Status>0 and a.Status!=8
end')
end

