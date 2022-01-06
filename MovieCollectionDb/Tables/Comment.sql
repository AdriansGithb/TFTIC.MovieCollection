CREATE TABLE [dbo].[Comment]
(
	[IdComment] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Com_Text] VARCHAR(MAX) NOT NULL, 
    [Com_IdMovie] INT NOT NULL, 
    [Com_CreatedBy] UNIQUEIDENTIFIER NOT NULL, 
    [Com_CreationDate] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [Com_LastModifBy] UNIQUEIDENTIFIER NULL, 
    [Com_LastModifDate] DATETIME2 NULL, 
    [Com_DeletedBy] UNIQUEIDENTIFIER NULL, 
    [Com_DeletionDate] DATETIME2 NULL, 
    CONSTRAINT [FK_Comment_AppUser_Creation] FOREIGN KEY (Com_CreatedBy) REFERENCES AppUser,    
    CONSTRAINT [FK_Comment_AppUser_Modification] FOREIGN KEY (Com_LastModifBy) REFERENCES AppUser,    
    CONSTRAINT [FK_Comment_AppUser_Deletion] FOREIGN KEY (Com_DeletedBy) REFERENCES AppUser, 
    CONSTRAINT [FK_Comment_Movie] FOREIGN KEY (Com_IdMovie) REFERENCES Movie
)