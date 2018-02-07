IF  not EXISTS (select * from schema_info where schema_major_version=2 and schema_minor_version=1)
begin
insert into schema_info values(2,1,'')
end

