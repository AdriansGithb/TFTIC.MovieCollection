# TFTIC.MovieCollection
Labo de renforcement : db SQL + Web API .NetCore (backEnd) + Asp .NetCore (FrontEnd)

Basé sur votre développement lors du laboratoire précédent (principalement Ado et Entities).



Il vous est demandé de mettre en place une application vous permettant de gérer votre collection de films :

Chaque film est représenté par son titre, son année de sortie, un bref synopsis, son réalisateur, son scénariste et son casting (2-3 acteurs par film)
Casting => Qui joue quel rôle dans quel film   (ex : Harisson Ford = Han Solo dans Star Wars et Indiana dans Indiana Jones)
Pour chaque personne (réalisateur/directeur/acteur) : son nom, prénom, date de naissance
ATTENTION : Une personne peut être acteur et/ou réalisateur et/ou scénariste d'un même film (exemple : Alexande Astier pour le film Kaamelott)
Il faudra respecter les consignes suivantes :

 - Une Web Api en .net Core

       Cette Api devra permettre :

                 - le CRUD sur les différentes entités  
                    Remarque: veuillez utiliser correctement les Verbs pour les différentes actions
                 - L'enregistrement d'un utilisateur ainsi que le concept de Login

                 - l'authentification et l'autorisation via un JWT   

                        2 rôles (User/Admin)

                                  User :  

                                            - peut commenter un film, accéder aux détails des films et des acteurs,... 

                                            - doit être en mesure de s'enregistrer et se connecter

                                Admin : Idem User + Supprimer les commentaires, ajouter/modifier un film et les différentes opération CUD

- Un Front en Asp.net Core

      - Le front-end doit être dans un projet séparé de l'Api Core

      - Le front-end NE PEUT PAS connaitre les Entities mais seulement les Models renvoyés par l'API



Dès que la matière Angular sera vue, il vous sera demandé de faire le front en Angular. Gardez donc cela en tête lors de votre développement afin de vous faciliter le changement de Front-End 
