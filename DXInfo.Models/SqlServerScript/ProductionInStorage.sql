IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionInStorage]') AND type in (N'U'))
begin
create table [dbo].[ProductionInStorage]
(
	Id int identity(1,1) not null PRIMARY KEY,	
	vcDeptId varchar(5) not null,
	InDate date not null,
	vcGoodsId varchar(10) not null,
	Quantity int not null,
	vcOperId varchar(10) not null,
	CreateDate datetime not null,
	CONSTRAINT AK_ProductionInStorage UNIQUE(vcDeptId,InDate,vcGoodsId) 
)
end