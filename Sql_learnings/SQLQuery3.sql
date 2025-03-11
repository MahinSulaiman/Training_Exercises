

CREATE PROCEDURE usp_InsertProject
	@ProjectName varchar(25),
	@EmployeeId int
AS
BEGIN
	INSERT INTO Projects (ProjectName,EmployeeId)
	VALUES (@ProjectName,@EmployeeId);
END;

EXEC usp_InsertProject 'FarmApp',5;



