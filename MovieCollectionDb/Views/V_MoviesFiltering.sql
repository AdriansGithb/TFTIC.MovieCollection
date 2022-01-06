CREATE VIEW [dbo].[V_MoviesFiltering]
	AS SELECT * 
	FROM Movie
		JOIN Country ON IdCountry = M_IdCountry
		JOIN Audience ON IdAudience = M_IdAudience
		JOIN Movie_Genre ON IdMovie = MG_IdMovie
		LEFT JOIN Acting ON IdMovie = Act_IdMovie
		LEFT JOIN Direction ON IdMovie = D_IdMovie
		LEFT JOIN Production ON IdMovie = P_IdMovie

