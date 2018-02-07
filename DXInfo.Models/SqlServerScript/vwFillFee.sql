IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[vwFillFee]') AND type in (N'V'))
begin
exec('CREATE view [dbo].[vwFillFee] as 
select iSerial,vcCardID,nFillFee,nFillProm,nFeeLast,nFeeCur,dtFillDate,vcComments,vcOperName,vcDeptID
from tbFillFee
union all
select iSerial,vcCardID,nFillFee,nFillProm,nFeeLast,nFeeCur,dtFillDate,vcComments,vcOperName,vcDeptID
from tbFillFeeOther
union all
select iSerial,vcCardID,nFillFee,nFillProm,nFeeLast,nFeeCur,dtFillDate,vcComments,vcOperName,vcDeptID
from tbFillFeeHis')
end

