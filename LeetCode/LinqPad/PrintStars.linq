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


with PrintStars as (
	select cast(replicate('*', 1) as nvarchar(20)) as stars, 2 as num	
	union all	
	select cast(replicate('*', num) as nvarchar(20)) as stars, num + 1 from PrintStars where num <= 20
)
select stars from PrintStars;
