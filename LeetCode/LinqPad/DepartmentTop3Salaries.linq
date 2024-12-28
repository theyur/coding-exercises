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

with ranked_salary as (select Department.Name as Department,
                              Employee.name as Employee,
							  salary,
                              dense_rank() over (partition by departmentId order by salary desc) as rank
                       from Employee inner join Department on Employee.departmentId = Department.id)
select Department, Employee, Salary
from ranked_salary
where ranked_salary.rank <= 3;
