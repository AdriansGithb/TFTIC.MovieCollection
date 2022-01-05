CREATE VIEW [dbo].[V_User]
	AS SELECT IdUser, U_Name, U_Email, U_IsAdmin, U_CreationDate, U_IsDeleted FROM AppUser
