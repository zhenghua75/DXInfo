IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_Balance_Report]') AND type in (N'P'))
begin
exec('
CREATE procedure [dbo].[proc_Balance_Report](
@BeginDate datetime,
@EndDate datetime,
@DeptId nvarchar(50))
as
begin

if   object_id(''tempdb..#temp_zhh_balancereport'') is not null  
        drop table #temp_zhh_balancereport 
create table #temp_zhh_balancereport
(
	LocalDeptId nvarchar(50) null,
	LocalDeptName nvarchar(50) null,
	RemoteDeptId nvarchar(50) null,
	RemoteDeptName nvarchar(50) null,
	FillFee_Pay numeric(38,2) null,
	FillProm_Pay numeric(38,2) null,
	Fee_Pay numeric(38,2) null,
	sumFee_Pay numeric(38,2) null,
	FillFee_Income numeric(38,2) null,
	FillProm_Income numeric(38,2) null,
	Fee_Income numeric(38,2) null,
	sumFee_Income numeric(38,2) null,
	FillFee_Dif numeric(38,2) null,
	FillProm_Dif numeric(38,2) null,
	Fee_Dif numeric(38,2) null,
	sumFee_Dif numeric(38,2) null
)

if @DeptId='''' or @DeptId =''00000000-0000-0000-0000-000000000000''
begin

insert into #temp_zhh_balancereport(LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName,
FillFee_Income,Fee_Income,sumFee_Income)
select LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName,
SUM(FIllFee) as FillFee_Income,SUM(Fee) Fee_Income,SUM(FillFee)+SUM(Fee) as sumFee_Income 
from BalanceProc where LocalDeptId=RemoteDeptId and BalanceDate between @BeginDate and @EndDate
group by LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName  order by LocalDeptId,LocalDeptName

insert into #temp_zhh_balancereport(LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName,
FillFee_Pay,Fee_Pay,sumFee_Pay)
select LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName,
-SUM(FIllFee) as FillFee_Pay,SUM(Fee) as Fee_Pay,-SUM(FillFee)+SUM(Fee) as sumFee_Pay 
from BalanceProc where LocalDeptId<>RemoteDeptId 
and BalanceDate between @BeginDate and @EndDate 
group by LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName  
order by LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName
end
else
begin

insert into #temp_zhh_balancereport(LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName,
FillFee_Income,Fee_Income,sumFee_Income)
select LocalDeptId,LocalDeptName,'''' as vcRemoteDeptId,'''' as vcRemoteDeptName,
SUM(FIllFee) as FillFee_Income,SUM(Fee) Fee_Income,SUM(FillFee)+SUM(Fee) as sumFee_Income 
from BalanceProc where LocalDeptId=RemoteDeptId and BalanceDate between @BeginDate and @EndDate and LocalDeptId=@DeptId
group by LocalDeptId,LocalDeptName  order by LocalDeptId,LocalDeptName

insert into #temp_zhh_balancereport(LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName,
FillFee_Income,sumFee_Income)
select LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName,
SUM(FIllFee) as FillFee_Income,SUM(FillFee) as sumFee_Income 
from BalanceProc where LocalDeptId<>RemoteDeptId 
and BalanceDate between @BeginDate and @EndDate and RemoteDeptId=@DeptId
group by LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName  
order by LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName

insert into #temp_zhh_balancereport(LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName,
Fee_Income,sumFee_Income)
select LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName,
SUM(Fee) Fee_Income,SUM(Fee) as sumFee_Income 
from BalanceProc where LocalDeptId<>RemoteDeptId 
and BalanceDate between @BeginDate and @EndDate and LocalDeptId=@DeptId
group by LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName  
order by LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName

insert into #temp_zhh_balancereport(LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName,
FillFee_Pay,sumFee_Pay)
select LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName,
SUM(FIllFee) as FillFee_Pay,SUM(FIllFee) as sumFee_Pay
from BalanceProc where LocalDeptId<>RemoteDeptId 
and BalanceDate between @BeginDate and @EndDate and LocalDeptId=@DeptId
group by LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName  
order by LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName

insert into #temp_zhh_balancereport(LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName,
Fee_Pay,sumFee_Pay)
select LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName,
SUM(Fee) Fee_Pay,SUM(Fee) as sumFee_Pay
from BalanceProc where LocalDeptId<>RemoteDeptId 
and BalanceDate between @BeginDate and @EndDate and RemoteDeptId=@DeptId
group by LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName  
order by LocalDeptId,LocalDeptName,RemoteDeptId,RemoteDeptName
end

select case when grouping(LocalDeptName)=1 then ''合计：'' else LocalDeptName end LocalDeptName,
    case when grouping(RemoteDeptName)=1 and grouping(LocalDeptName)=0 then ''小计：'' else RemoteDeptName end RemoteDeptName,
    
sum(FillFee_Pay) as FillFee_Pay,sum(FillProm_Pay) as FillProm_Pay,sum(Fee_Pay) as Fee_Pay,sum(sumFee_Pay) as sumFee_Pay,
sum(FillFee_Income) as FillFee_Income,sum(FillProm_Income) as FillProm_Income,sum(Fee_Income) as Fee_Income,sum(sumFee_Income) as sumFee_Income,
isnull(sum(FillFee_Income),0)-isnull(sum(FillFee_Pay),0) as FillFee_Dif,
isnull(sum(FillProm_Income),0)-isnull(sum(FillProm_Pay),0) as FillProm_DIf,
isnull(sum(Fee_Income),0)-isnull(sum(Fee_Pay),0) as Fee_Dif,
isnull(sum(sumFee_Income),0)-isnull(sum(sumFee_Pay),0) as sumFee_Dif
 from #temp_zhh_balancereport
group by LocalDeptName,RemoteDeptName with rollup 
end
')
end