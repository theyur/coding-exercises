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

with GroupedMin as (select player_id, min(event_date) as min_date
                    from Activity
                    group by player_id)
select convert(numeric(10, 2), count(a.player_id) * 1.0 / (select count(distinct player_id) from Activity)) as fraction
from Activity a,
     GroupedMin g
where a.player_id = g.player_id
  and datediff(day, g.min_date, a.event_date) = 1

/*
with GroupedConsec as (select distinct Activity.player_id as player_id, Activity.event_date as event_date
                       from (select player_id, event_date from Activity) a
                                join Activity
                                     on Activity.player_id = a.player_id
                                         and datediff(day, Activity.event_date, a.event_date) = 1),
     GroupedMin as (select Activity.player_id, min(Activity.event_date) as min_date
                    from Activity
                    group by Activity.player_id),
     Refined as (select distinct GroupedConsec.player_id
                 from GroupedConsec
                          join GroupedMin
                               on GroupedConsec.player_id = GroupedMin.player_id and GroupedConsec.event_date = GroupedMin.min_date),
     Total as (select count(distinct player_id) as count from Activity)
select convert(numeric(10, 2), count(Refined.player_id) * 1.0 / Total.count) as fraction
from Refined,
     Total
group by Total.count;
*/