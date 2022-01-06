CREATE PROCEDURE [dbo].[UserUpdate]
	@email VARCHAR(100),
	@name VARCHAR(50),
	@password VARCHAR(50),
	@userid UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @salt VARCHAR(100)
	SELECT @salt = S_Value 
		FROM Salt 
		WHERE S_IdUser = @userid

	DECLARE @secretkey VARCHAR(100)
	SET @secretkey = dbo.GetSecretKey()

	DECLARE @pass_hash VARBINARY(64)
	SET @pass_hash = HASHBYTES('SHA2_512', CONCAT(@salt, @secretkey, @password, @salt))

	DECLARE @pass_verif VARBINARY(64)
	SELECT @pass_verif = U_Password 
		FROM AppUser 
		WHERE IdUser = @userid

	IF(@pass_hash != @pass_verif)
	BEGIN
		RAISERROR('le mot de passe est erronné, mise à jour impossible',16,1)
		ROLLBACK
	END

	UPDATE AppUser SET U_Email = @email, U_Name = @name WHERE IdUser = @userid

END