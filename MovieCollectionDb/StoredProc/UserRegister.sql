CREATE PROCEDURE [dbo].[UserRegister]
	@email VARCHAR(100),
	@password VARCHAR(50),
	@name VARCHAR(50)
AS
BEGIN
	DECLARE @salt VARCHAR(100)
	SET @salt = CONCAT(NEWID(), NEWID(), NEWID())

	DECLARE @secretkey VARCHAR(100)
	SET @secretkey = dbo.GetSecretKey()

	DECLARE @pass_hash VARBINARY(64)
	SET @pass_hash = HASHBYTES('SHA2_512', CONCAT(@salt, @secretkey, @password, @salt))

	CREATE TABLE #tempTab (lastId UNIQUEIDENTIFIER)

	INSERT INTO AppUser (U_Email, U_Password, U_Name) 
		OUTPUT inserted.IdUser INTO #tempTab
		VALUES (@email, @pass_hash, @name)

	INSERT INTO Salt VALUES ((SELECT * FROM #tempTab), @salt)

	DROP TABLE #tempTab

END