IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[vwConsItem]') AND type in (N'V'))
begin
exec('CREATE  view [dbo].[vwConsItem] as 
select iSerial,vcGoodsID,vcCardID,nPrice,iCount,nTRate,nFee,vcComments,cFlag,dtConsDate,vcOperName,vcDeptID
from tbConsItem
union all
select iSerial,vcGoodsID,vcCardID,nPrice,iCount,nTRate,nFee,vcComments,cFlag,dtConsDate,vcOperName,vcDeptID
from tbConsItemOther
union all
select iSerial,vcGoodsID,vcCardID,nPrice,iCount,nTRate,nFee,vcComments,cFlag,dtConsDate,vcOperName,vcDeptID
from dbo.tbConsItemHis')
end