<Query Kind="SQL">
  <Connection>
    <ID>c114aa9c-796b-487e-8261-b29bac53602a</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>localhost</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Database>Test_Entities</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

with source as (
	select top 1 s1.id, s1.visit_date, s1.people 
	from Stadium s1 join Stadium s2 on s2.id - s1.id = 1
	where s1.people >= 100

	union
	
	select s2.id, s2.visit_date, s2.people 
	from Stadium s1 join Stadium s2 on s2.id - s1.id = 1
	where s2.people >= 100
)
--select * from source

,base as (
	select id, visit_date, people 
	,id - row_number() over (order by id) as row_num
	from source
)
--select * from base

,counted as (
	select id, visit_date, people, row_num,
	count(*) over (partition by row_num) as row_count
	from base
)
select id, visit_date, people 
from counted
where row_count >= 3
