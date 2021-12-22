CREATE TABLE [dbo].[Movie_Genre]
(
	[MG_IdGenre] INT NOT NULL , 
    [MG_IdMovie] INT NOT NULL, 
    PRIMARY KEY ([MG_IdMovie],[MG_IdGenre]), 
    CONSTRAINT [FK_Movie_Genre_Genre] FOREIGN KEY (MG_IdGenre) REFERENCES Genre, 
    CONSTRAINT [FK_Movie_Genre_Movie] FOREIGN KEY (MG_IdMovie) REFERENCES Movie
)
