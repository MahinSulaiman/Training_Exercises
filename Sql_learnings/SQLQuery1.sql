CREATE DATABASE claysysTraining;
USE claysysTraining;

CREATE TABLE Employee(
	EmployeeId int PRIMARY KEY IDENTITY(1,1),
	FirstName varchar(25) NOT NULL,
	LastName varchar(25),
	EmailId varchar(25) UNIQUE,
	Age int CHECK(Age>=18),
	Salary DECIMAL(10,2) DEFAULT 25000.00,
	JoiningDate DATE,
);

SELECT * FROM Employee;


INSERT INTO Employee(FirstName,LastName,EmailId,Age,Salary,JoiningDate) 
VALUES ('Antony','Don','antony@gmail.com',20,20000,'2024-11-11');
INSERT INTO Employee(FirstName,LastName,EmailId,Age,JoiningDate) 
VALUES ('Ajith','Kumar','ajith@gmail.com',20,'2020-12-10');

ALTER TABLE Employee
ADD Department varchar(30);

EXEC sp_rename 'Employee.Department','City','COLUMN';

ALTER TABLE Employee
DROP COLUMN City;

ALTER TABLE Employee
ALTER COLUMN Age int NOT NULL;

UPDATE Employee
SET Department='Development'
WHERE EmployeeId=1;

UPDATE Employee
SET Age=22
WHERE EmployeeId=1;

SELECT Employee.FirstName FROM Employee
WHERE Salary=(SELECT MAX(Salary) FROM Employee);

SELECT MAX(Salary) AS second_highest_salary
FROM Employee
WHERE Salary < (SELECT MAX(Salary) FROM Employee);

SELECT Department,COUNT(EmployeeID) AS Employes
FROM Employee GROUP BY Department;

SELECT * FROM Employee ORDER BY Age;

SELECT Department,COUNT(EmployeeID) AS Employes
FROM Employee GROUP BY Department having COUNT(EmployeeID)>2;


DROP TABLE Projects;
CREATE TABLE Projects(
	ProjectID int PRIMARY KEY IDENTITY(500,1),
	ProjectName varchar(25),
	EmployeeId int REFERENCES Employee(EmployeeId)
);

INSERT INTO Projects(ProjectName) 
VALUES('Auditing');

SELECT Employee.FirstName,Projects.ProjectName
FROM Employee
JOIN Projects ON Employee.EmployeeId=Projects.EmployeeId; 

SELECT Employee.FirstName,Projects.ProjectName
FROM Employee
LEFT JOIN Projects ON Employee.EmployeeId=Projects.EmployeeId; 

SELECT Employee.FirstName,Projects.ProjectName
FROM Employee
RIGHT JOIN Projects ON Employee.EmployeeId=Projects.EmployeeId; 

SELECT Employee.FirstName,Projects.ProjectName
FROM Employee
FULL JOIN Projects ON Employee.EmployeeId=Projects.EmployeeId; 


