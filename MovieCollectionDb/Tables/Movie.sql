CREATE TABLE [dbo].[Movie]
(
	[IdMovie] INT NOT NULL PRIMARY KEY IDENTITY, 
    [M_Title] NVARCHAR(200) NOT NULL, 
    [M_ReleaseYear] INT NOT NULL, 
    [M_Synopsys] NVARCHAR(MAX) NULL, 
    [M_TrailerLink] NVARCHAR(MAX) NULL, 
    [M_IsDeleted] BIT NOT NULL DEFAULT 0, 
    [M_IdCountry] INT NULL, 
    [M_IdAudience] INT NULL, 
    CONSTRAINT [CK_Movie_ReleaseYear] CHECK (M_ReleaseYear BETWEEN 1700 AND (YEAR(GETDATE())+1)), 
    CONSTRAINT [FK_Movie_Country] FOREIGN KEY (M_IdCountry) REFERENCES Country, 
    CONSTRAINT [FK_Movie_Audience] FOREIGN KEY (M_IdAudience) REFERENCES Audience,
    CONSTRAINT UK_Movie_YearTitle UNIQUE (M_Title, M_ReleaseYear)
)
