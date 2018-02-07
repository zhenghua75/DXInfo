if not exists (select * from PTType where Code='001')insert into PTType(Code,Name,RdCode,IsDefault)values('001','普通采购','001',1)
