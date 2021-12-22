CREATE TABLE [dbo].[Artist]
(
	[IdArtist] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Art_Name] NVARCHAR(50) NOT NULL, 
    [Art_FirstName] NVARCHAR(50) NOT NULL, 
    [Art_BirthDate] DATETIME2 NULL,
    
    CONSTRAINT UK_UniqueArtist UNIQUE ([Art_Name], [Art_FirstName], [Art_BirthDate]) 
)

GO
