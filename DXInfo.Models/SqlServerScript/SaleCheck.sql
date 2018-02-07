IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaleCheck]') AND type in (N'U'))
begin
create table [dbo].[SaleCheck]
(
	Id int identity(1,1) not null PRIMARY KEY,	
	vcDeptId varchar(5) not null,
	CheckDate date not null,
	vcGoodsId varchar(10) not null,
	Quantity int not null,
	vcOperId varchar(10) not null,
	CreateDate datetime not null,
	CONSTRAINT AK_SaleCheck UNIQUE(vcDeptId,CheckDate,vcGoodsId) 
)
end