﻿if not exists (select * from RdType where Code='001')insert into RdType(Code,Name,Flag)values('001','采购入库',0)
if not exists (select * from RdType where Code='002')insert into RdType(Code,Name,Flag)values('002','销售出库',1)
if not exists (select * from RdType where Code='003')insert into RdType(Code,Name,Flag)values('003','调拨入库',0)
if not exists (select * from RdType where Code='004')insert into RdType(Code,Name,Flag)values('004','盘盈入库',0)
if not exists (select * from RdType where Code='005')insert into RdType(Code,Name,Flag)values('005','其它入库',0)
if not exists (select * from RdType where Code='006')insert into RdType(Code,Name,Flag)values('006','调拨出库',1)
if not exists (select * from RdType where Code='007')insert into RdType(Code,Name,Flag)values('007','盘亏出库',1)
if not exists (select * from RdType where Code='008')insert into RdType(Code,Name,Flag)values('008','不合格品',1)
if not exists (select * from RdType where Code='009')insert into RdType(Code,Name,Flag)values('009','其它出库',1)
if not exists (select * from RdType where Code='010')insert into RdType(Code,Name,Flag)values('010','材料出库',1)
if not exists (select * from RdType where Code='011')insert into RdType(Code,Name,Flag)values('011','成品入库',0)
if not exists (select * from RdType where Code='012')insert into RdType(Code,Name,Flag)values('012','期初库存',0)