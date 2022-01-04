CREATE TABLE [dbo].[Direction]
(
	[D_IdArtist] INT NOT NULL , 
    [D_IdMovie] INT NOT NULL, 
    PRIMARY KEY ([D_IdArtist],[D_IdMovie]), 
    CONSTRAINT [FK_Direction_Artist] FOREIGN KEY (D_IdArtist) REFERENCES Artist ON DELETE CASCADE, 
    CONSTRAINT [FK_Direction_Movie] FOREIGN KEY (D_IdMovie) REFERENCES Movie
)
