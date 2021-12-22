/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO Artist (Art_Name, Art_FirstName, Art_BirthDate) VALUES 
('Eastwood', 'Clint', '1930-01-31'),
('Harrison', 'Ford','1942-07-13'),
('McGregor', 'Ewan', '1971-03-31'),
('Willis', 'Bruce', '1955-03-19'),
('Lucas','Georges','1944-05-14')

INSERT INTO Country(C_Name) VALUES ('Belgique'),('USA'),('France'), ('Allemagne'), ('Angleterre'), ('Espagne')

INSERT INTO Genre(G_Label) VALUES ('Comedie'), ('Science-Fiction'), ('Drame'), ('Action'), ('Romantique'), ('Politique'), ('Animation')

INSERT INTO Audience(Au_Label) VALUES ('Tout public'),('-18'), ('-16'), ('-12'), ('-6')

INSERT INTO Movie(M_Title, M_Synopsys, M_TrailerLink, M_ReleaseYear, M_IdCountry, M_IdAudience) VALUES
('Star Wars Episode 1', 'Le premier episode de star wars', 'https://www.youtube.com/watch?v=bD7bpG-zDJQ', 1999, 2, 4),
('Star Wars Episode 2', 'Le 2e episode de star wars', 'https://www.youtube.com/watch?v=gYbW1F_c9eM', 2002, 2, 4),
('Die Hard : Piège de cristal', 'Le premier film de la saga Die Hard','https://www.youtube.com/watch?v=jaJuwKCmJbY',1988,2,3)

INSERT INTO Movie_Genre VALUES (2,1),(2,2),(4,3)

INSERT INTO Acting VALUES (3,1,'Obi-Wan Kenobi'),(3,2,'Obi-Wan Kenobi'), (4,3,'John McClane')

INSERT INTO Production VALUES (5,1), (5,2), (1,3)

INSERT INTO Direction VALUES (5,1),(5,2)
