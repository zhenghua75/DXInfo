IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[vwBusiLog]') AND type in (N'V'))
begin
exec('CREATE  view [dbo].[vwBusiLog] as 
select iSerial,vcCardID,vcOperType,vcOperName,dtOperDate,vcComments,vcDeptID
from tbBusiLog
union all
select iSerial,vcCardID,vcOperType,vcOperName,dtOperDate,vcComments,vcDeptID
from tbBusiLogOther
union all
select iSerial,vcCardID,vcOperType,vcOperName,dtOperDate,vcComments,vcDeptID
from tbBusiLogHis')
end