CREATE TABLE [dbo].[Country]
(
	[IdCountry] INT NOT NULL PRIMARY KEY IDENTITY, 
    [C_Name] NVARCHAR(50) NOT NULL,

	CONSTRAINT UK_Country_Name UNIQUE (C_Name)
)
