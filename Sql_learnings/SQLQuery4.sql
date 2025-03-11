

CREATE PROCEDURE usp_SelectProject
	@ProjectId int
AS
BEGIN
	SELECT * FROM Projects
	WHERE ProjectId=@ProjectId;
END;

EXEC usp_SelectProject 501;





		