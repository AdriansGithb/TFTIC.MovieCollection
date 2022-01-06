CREATE TABLE [dbo].[Audience]
(
	[IdAudience] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Au_Label] NVARCHAR(50) NOT NULL,

	CONSTRAINT UK_Audience_Label UNIQUE (Au_Label)
)
