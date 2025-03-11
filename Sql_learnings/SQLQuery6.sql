

CREATE PROCEDURE usp_CrudProjects
	@Operation varchar(10),
	@ProjectId int=NULL,
	@ProjectName varchar(25)=NULL,
	@EmployeeId int=NULL
AS
BEGIN
	IF @Operation='INSERT'
	BEGIN
		INSERT INTO Projects(ProjectName,EmployeeId)
		VALUES (@ProjectName,@EmployeeId);
	END

	ELSE IF @Operation='UPDATE'
	BEGIN
		UPDATE Projects
		SET ProjectName=@ProjectName,EmployeeId=@EmployeeId
		WHERE ProjectId=@ProjectId;
	END

	ELSE IF @Operation='SELECT'
	BEGIN
		SELECT * FROM Projects
		WHERE ProjectId=@ProjectId;
	END

	ELSE IF @Operation='DELETE'
	BEGIN
		DELETE FROM Projects
		WHERE ProjectId=@ProjectId;
	END

	ELSE
	BEGIN
		RAISERROR('Ivalid action',16,1);
	END
END;

EXECUTE usp_CrudProjects @Operation='INSERT',
						 @ProjectName='SmpleApp',
						 @EmployeeId=4;


EXECUTE usp_CrudProjects @Operation='UPDATE',
						 @ProjectName='TestApp',
						 @EmployeeId=3,
						 @ProjectId=510;

EXECUTE usp_CrudProjects @Operation='SELECT',
						 @ProjectId=510;


EXECUTE usp_CrudProjects @Operation='DELETE',
						 @ProjectId=510;


				
		
	

  SELECT FirstName FROM Employee
  WHERE Age=(SELECT MAX(Age) FROM Employee);

  
  SELECT TOP 1 Department,COUNT(EmployeeId) AS number FROM Employee
  GROUP BY Department ORDER BY number DESC ;


