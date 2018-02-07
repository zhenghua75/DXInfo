IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[vwBill]') AND type in (N'V'))
begin
exec('CREATE view [dbo].[vwBill] as 
select iSerial,vcCardID,nTRate,nFee,nPay,nBalance,iIgValue,vcConsType,vcOperName,dtConsDate,vcDeptID
from tbBill
union all
select iSerial,vcCardID,nTRate,nFee,nPay,nBalance,iIgValue,vcConsType,vcOperName,dtConsDate,vcDeptID
from tbBillOther
union all
select iSerial,vcCardID,nTRate,nFee,nPay,nBalance,iIgValue,vcConsType,vcOperName,dtConsDate,vcDeptID
from tbBillHis')
end
