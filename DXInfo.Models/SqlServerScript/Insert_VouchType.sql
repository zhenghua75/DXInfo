﻿if not exists (select * from VouchType where Code='001')insert into VouchType(Code,Name)values('001','采购入库单')
if not exists (select * from VouchType where Code='002')insert into VouchType(Code,Name)values('002','销售出库单')
if not exists (select * from VouchType where Code='003')insert into VouchType(Code,Name)values('003','其它入库单')
if not exists (select * from VouchType where Code='004')insert into VouchType(Code,Name)values('004','其它出库单')
if not exists (select * from VouchType where Code='005')insert into VouchType(Code,Name)values('005','材料出库单')
if not exists (select * from VouchType where Code='006')insert into VouchType(Code,Name)values('006','产成品入库单')
if not exists (select * from VouchType where Code='007')insert into VouchType(Code,Name)values('007','期初库存')
if not exists (select * from VouchType where Code='008')insert into VouchType(Code,Name)values('008','不合格品记录单')
if not exists (select * from VouchType where Code='009')insert into VouchType(Code,Name)values('009','调拨单')
if not exists (select * from VouchType where Code='010')insert into VouchType(Code,Name)values('010','盘点单')
if not exists (select * from VouchType where Code='011')insert into VouchType(Code,Name)values('011','货位调整单')
if not exists (select * from VouchType where Code='012')insert into VouchType(Code,Name)values('012','配料单')