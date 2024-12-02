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

select * from Department;
select * from Employee;

--select max(salary)
--	from Employee inner join Department on Employee.departmentId = Department.id 
--	group by departmentId

with salaryGrouped as (
	select departmentId, max(salary) as max_salary
		from Employee join Department on Employee.departmentId = Department.id 
		group by departmentId
),
employeePartitioned as (
	select departmentId, Department.name as dName, Employee.name as eName, salary,
		max(salary) over (partition by departmentId) max_salary
		from Employee inner join Department on Employee.departmentId = Department.id
)
select employeePartitioned.dName as Department, employeePartitioned.eName as Employee, employeePartitioned.salary
	from salaryGrouped join employeePartitioned on salaryGrouped.departmentId = employeePartitioned.departmentId
	where employeePartitioned.salary = salaryGrouped.max_salary
	
	
