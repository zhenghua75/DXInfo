IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DXInfo_GetCurrentInvLocatorOfCheck]') AND type in (N'P'))
drop procedure dbo.sp_DXInfo_GetDeptInventory
IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DXInfo_GetCurrentInvLocatorOfCheck]') AND type in (N'P'))
begin
exec('
create procedure [dbo].[sp_DXInfo_GetCurrentInvLocatorOfCheck]
@WhId uniqueidentifier
as
begin
select * from CurrentInvLocator e
left join
(select c.InvId,c.Batch from 
(select a.InvId,a.Batch from CheckVouchs a
left join CheckVouch b on a.cvid=b.Id
where b.WhId=@WhId and b.IsVerify=1 and a.CNum=0
group by a.InvId,a.Batch) c
join
(select InvId,Batch from CurrentInvLocator where WhId=@WhId and Num=0 
group by InvId,Batch) d on c.InvId=d.InvId and c.Batch=d.Batch
) f on e.InvId=f.InvId and e.Batch=f.Batch
where e.WhId=@WhId and f.InvId is null
end')
end