﻿if not exists (select * from EnumTypeDescription where Code='CheckCycle' and Value=0)insert into EnumTypeDescription(Code,Value,Description)values('CheckCycle',0,'天')
if not exists (select * from EnumTypeDescription where Code='CheckCycle' and Value=1)insert into EnumTypeDescription(Code,Value,Description)values('CheckCycle',1,'周')
if not exists (select * from EnumTypeDescription where Code='CheckCycle' and Value=2)insert into EnumTypeDescription(Code,Value,Description)values('CheckCycle',2,'月')
if not exists (select * from EnumTypeDescription where Code='MeasurementUnitGroupCategory' and Value=0)insert into EnumTypeDescription(Code,Value,Description)values('MeasurementUnitGroupCategory',0,'无换算')
if not exists (select * from EnumTypeDescription where Code='MeasurementUnitGroupCategory' and Value=1)insert into EnumTypeDescription(Code,Value,Description)values('MeasurementUnitGroupCategory',1,'浮动换算')
if not exists (select * from EnumTypeDescription where Code='MeasurementUnitGroupCategory' and Value=2)insert into EnumTypeDescription(Code,Value,Description)values('MeasurementUnitGroupCategory',2,'固定换算')
if not exists (select * from EnumTypeDescription where Code='ProductType' and Value=0)insert into EnumTypeDescription(Code,Value,Description)values('ProductType',0,'原材料')
if not exists (select * from EnumTypeDescription where Code='ProductType' and Value=1)insert into EnumTypeDescription(Code,Value,Description)values('ProductType',1,'产成品')
if not exists (select * from EnumTypeDescription where Code='ShelfLifeType' and Value=0)insert into EnumTypeDescription(Code,Value,Description)values('ShelfLifeType',0,'天')
if not exists (select * from EnumTypeDescription where Code='ShelfLifeType' and Value=1)insert into EnumTypeDescription(Code,Value,Description)values('ShelfLifeType',1,'周')
if not exists (select * from EnumTypeDescription where Code='ShelfLifeType' and Value=2)insert into EnumTypeDescription(Code,Value,Description)values('ShelfLifeType',2,'月')
if not exists (select * from EnumTypeDescription where Code='ShelfLifeType' and Value=3)insert into EnumTypeDescription(Code,Value,Description)values('ShelfLifeType',3,'年')
if not exists (select * from EnumTypeDescription where Code='ValueType' and Value=0)insert into EnumTypeDescription(Code,Value,Description)values('ValueType',0,'个别计价')