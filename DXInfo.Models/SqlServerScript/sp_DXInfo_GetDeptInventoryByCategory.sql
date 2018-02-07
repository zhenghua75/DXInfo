IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DXInfo_GetDeptInventoryByCategory]') AND type in (N'P'))
drop procedure dbo.sp_DXInfo_GetDeptInventoryByCategory
IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DXInfo_GetDeptInventoryByCategory]') AND type in (N'P'))
begin
exec('create procedure dbo.sp_DXInfo_GetDeptInventoryByCategory
@DeptId uniqueidentifier,
@InvType int,
@IsDeptPrice bit,
@Category uniqueidentifier
as
begin
select 
a.Id,
a.Code,
a.Name,
a.Category,
case when @IsDeptPrice=1 and c.SalePoint is not null and c.SalePoint>0 then c.SalePoint else a.SalePoint end SalePoint,
case when @IsDeptPrice=1 and c.SalePoint0 is not null and c.SalePoint0>0 then c.SalePoint0 else a.SalePoint0 end SalePoint0,
case when @IsDeptPrice=1 and c.SalePoint1 is not null and c.SalePoint1>0 then c.SalePoint1 else a.SalePoint1 end SalePoint1,
case when @IsDeptPrice=1 and c.SalePoint2 is not null and c.SalePoint2>0 then c.SalePoint2 else a.SalePoint2 end SalePoint2,
case when @IsDeptPrice=1 and c.SalePrice is not null and c.SalePrice>0 then c.SalePrice else a.SalePrice end SalePrice,
case when @IsDeptPrice=1 and c.SalePrice0 is not null and c.SalePrice0>0 then c.SalePrice0 else a.SalePrice0 end SalePrice0,
case when @IsDeptPrice=1 and c.SalePrice1 is not null and c.SalePrice1>0 then c.SalePrice1 else a.SalePrice1 end SalePrice1,
case when @IsDeptPrice=1 and c.SalePrice2 is not null and c.SalePrice2>0 then c.SalePrice2 else a.SalePrice2 end SalePrice2,
a.Printer,a.IsPackage,a.UnitOfMeasure,
a.Specs,a.Comment,a.ImageFileName,a.IsDonate,a.Stars,a.Feature,a.Dosage,a.Palette,a.EnglishName,a.IsRecommend,a.EnglishIntroduce,a.EnglishDosage,a.InvType,a.IsInValid,a.Sort,a.MeasurementUnitGroup,a.MainUnit,a.UnitCategory,
a.PurchaseUnit,a.StockUnit,a.ValueType,a.HighStock,a.LowStock,a.SecurityStock,a.Locator,a.LastCheckDate,a.CheckCycle,a.SomeDay,a.IsShelfLife,a.ShelfLife,a.EarlyWarningDay,a.IsBatch,a.ShelfLifeType,
a.IsSale,a.WhId,a.Karat,a.MetalWeight,a.JewelWeight,a.TotalWeight,a.QTY,a.OrderNo,a.VendorSpec,a.Length
 from Inventory a
left join InvDepts b on a.Id=b.Inv
left join (select * from InventoryDeptPrice where DeptId=@DeptId) c on a.Id =c.InvId
where b.Dept=@DeptId
and a.IsInvalid=0 and a.InvType=@InvType
and a.Category=@Category
order by a.Code
end')
end