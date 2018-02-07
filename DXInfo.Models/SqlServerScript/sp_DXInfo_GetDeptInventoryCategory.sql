IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DXInfo_GetDeptInventoryCategory]') AND type in (N'P'))
drop procedure dbo.sp_DXInfo_GetDeptInventoryCategory
IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DXInfo_GetDeptInventoryCategory]') AND type in (N'P'))
begin
exec('create procedure dbo.sp_DXInfo_GetDeptInventoryCategory
@DeptId uniqueidentifier,
@CategoryType int
as
begin
select * from InventoryCategory a
left join CategoryDepts b on a.Id=b.Category
where b.Dept=@DeptId
and a.CategoryType=@CategoryType
and a.Id in (
select distinct Category from Inventory c
left join InvDepts d on c.Id=d.Inv
where d.Dept=@DeptId
and IsInvalid=0
)
order by a.Code
end')
end