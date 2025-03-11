CREATE PROCEDURE usp_UpdateProject
	@ProjectId int,
	@ProjectName varchar(25),
	@EmployeeId int
AS
BEGIN
	UPDATE Projects
	SET ProjectName=@ProjectName,
		EmployeeId=@EmployeeId
	WHERE ProjectId=@ProjectId;
END;

EXEC usp_UpdateProject 508,'CRUDApp',1;

