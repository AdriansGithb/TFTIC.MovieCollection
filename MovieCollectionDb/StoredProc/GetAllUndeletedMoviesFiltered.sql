CREATE PROCEDURE [dbo].[GetAllUndeletedMoviesFiltered]
	@idGenre int = NULL,
	@idArtist int = NULL,
	@idCountry int = NULL,
	@idAudience int = NULL
AS
BEGIN
	
	SELECT * 
		FROM V_MoviesFiltering
		WHERE (@idGenre = MG_IdGenre OR @idGenre IS NULL)
			AND (@idCountry = M_IdCountry OR @idCountry IS NULL)
			AND (@idAudience = M_IdAudience OR @idAudience IS NULL)
			AND ((COALESCE(@idArtist, Act_IdArtist) = Act_IdArtist) 
				OR (COALESCE(@idArtist, D_IdArtist) = D_IdArtist )
				OR (COALESCE(@idArtist, P_IdArtist) = P_IdArtist ))
			AND M_IsDeleted = 0

END