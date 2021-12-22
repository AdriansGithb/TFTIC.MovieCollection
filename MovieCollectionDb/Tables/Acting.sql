CREATE TABLE [dbo].[Acting]
(
	[Act_IdArtist] INT NOT NULL , 
    [Act_IdMovie] INT NOT NULL, 
    PRIMARY KEY ([Act_IdArtist],[Act_IdMovie]), 
    CONSTRAINT [FK_Acting_Artist] FOREIGN KEY (Act_IdArtist) REFERENCES Artist, 
    CONSTRAINT [FK_Acting_Movie] FOREIGN KEY (Act_IdMovie) REFERENCES Movie
)
