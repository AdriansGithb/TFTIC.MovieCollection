CREATE PROCEDURE [dbo].[UserPasswordUpdate]
	@actualPassword VARCHAR(50),
	@newPassword VARCHAR(50),
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
	SET @pass_hash = HASHBYTES('SHA2_512', CONCAT(@salt, @secretkey, @actualPassword, @salt))

	DECLARE @pass_verif VARBINARY(64)
	SELECT @pass_verif = U_Password 
		FROM AppUser 
		WHERE IdUser = @userid

	IF(@pass_hash != @pass_verif)
	BEGIN
		RAISERROR('le mot de passe est erronné, mise à jour impossible',16,1)
		ROLLBACK
	END

	SET @pass_hash = HASHBYTES('SHA2_512', CONCAT(@salt, @secretkey, @newPassword, @salt))

	UPDATE AppUser SET U_Password = @pass_hash WHERE IdUser = @userid

END