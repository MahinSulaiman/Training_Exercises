CREATE TABLE Projects(
	ProjectID int PRIMARY KEY IDENTITY(500,1),
	ProjectName varchar(25),
	EmployeeId int REFERENCES Employee(EmployeeId)
);

SELECT * FROM Projects;

INSERT INTO Projects(ProjectName,EmployeeId) 
VALUES('BusinessAnalysis',7);