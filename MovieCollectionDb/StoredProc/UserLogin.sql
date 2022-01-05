CREATE PROCEDURE [dbo].[UserLogin]
	@email VARCHAR(100),
	@password VARCHAR(50)
AS
BEGIN
	DECLARE @salt VARCHAR(100)
	SELECT @salt = S_Value 
		FROM AppUser JOIN Salt ON IdUser = S_IdUser
		WHERE U_Email = @email

	DECLARE @secretkey VARCHAR(100)
	SET @secretkey = dbo.GetSecretKey()

	DECLARE @pass_hash VARBINARY(64)
	SET @pass_hash = HASHBYTES('SHA2_512', CONCAT(@salt, @secretkey, @password, @salt))

	DECLARE @id UNIQUEIDENTIFIER
	SELECT @id = IdUser 
		FROM AppUser 
		WHERE U_Email = @email 
			AND U_Password = @pass_hash

	SELECT * FROM V_User WHERE IdUser = @id

END