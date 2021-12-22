CREATE TABLE [dbo].[Production]
(
	[P_IdArtist] INT NOT NULL , 
    [P_IdMovie] INT NOT NULL, 
    PRIMARY KEY ([P_IdArtist],[P_IdMovie]), 
    CONSTRAINT [FK_Production_Artist] FOREIGN KEY (P_IdArtist) REFERENCES Artist, 
    CONSTRAINT [FK_Production_Movie] FOREIGN KEY (P_IdMovie) REFERENCES Movie
)
