IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_balance_bydate]') AND type in (N'P'))
begin
exec('
CREATE procedure [dbo].[proc_balance_bydate](@BeginDate date,@EndDate date)
as
begin
delete from BalanceProc where BalanceDate between @BeginDate and @EndDate
 create table #temp_zhh_balanceproc
(
	LocalDeptId uniqueidentifier not  null,
	LocalDeptName nvarchar(256) not  null,
	RemoteDeptId uniqueidentifier not null,
	RemoteDeptName nvarchar(256) not null,
	BalanceDate date not null,
	FillFee numeric(24,2) not null,
	Fee numeric(24,2) not null,
	FillProm numeric(24,2) not null
)

insert into #temp_zhh_balanceproc
select b.LocalDeptId,b.LocalDeptName ,a.RemoteDeptId,a.RemoteDeptName,convert(varchar(10),a.CreateDate,121) as BalanceDate,sum(a.Amount) as FillFee,0 as Fee,sum(a.Donate) as FillProm
from (select fillFee.DeptID as RemoteDeptId,c.DeptName as RemoteDeptName,Amount,Donate,Card,fillFee.CreateDate from Recharges fillFee
left join (select DeptId,DeptName from Depts) c on fillFee.DeptID=c.DeptID
 where convert(varchar(10),CreateDate,121) between convert(varchar(10),@BeginDate,121) and convert(varchar(10),@EndDate,121)) a 
left join ( select ass.DeptId as LocalDeptId,d.DeptName as LocalDeptName,Id as Card from  Cards ass
left join (select DeptId,DeptName from Depts ) d on ass.DeptID=d.DeptID)b
on a.Card=b.Card
group by b.LocalDeptId,b.LocalDeptName,a.RemoteDeptId,a.RemoteDeptName,convert(varchar(10),a.CreateDate,121)

insert into #temp_zhh_balanceproc
 select b.LocalDeptId,b.LocalDeptName,a.RemoteDeptId,a.RemoteDeptName,convert(varchar(10),a.CreateDate,121) as dtBalanceDate,0 as nFillFee,SUM(a.Amount) as Fee ,0 as nFillProm
 from (select cons.Card,cons.DeptId as RemoteDeptId,c.DeptName as RemoteDeptName,cons.Amount,cons.CreateDate from Consume cons
left join (select DeptId,DeptName from Depts ) c on cons.DeptID=c.DeptID
where cons.ConsumeType=0 and convert(varchar(10),cons.CreateDate,121) between convert(varchar(10),@BeginDate,121) and convert(varchar(10),@EndDate,121)) a 
left join 
( select ass.DeptId as LocalDeptId,d.DeptName as LocalDeptName,Id as Card from  Cards ass
left join (select DeptId,DeptName from Depts ) d on ass.DeptID=d.DeptID)b
on a.Card=b.Card
group by b.LocalDeptId,b.LocalDeptName,a.RemoteDeptId,a.RemoteDeptName,CONVERT(varchar(10),a.CreateDate,121)

insert into BalanceProc
select LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName,BalanceDate,SUM(FillFee) as FillFee,SUM(Fee) as Fee,sum(FillProm) as FillProm
from #temp_zhh_balanceproc
group by LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName,BalanceDate
drop table #temp_zhh_balanceproc
end
')
end