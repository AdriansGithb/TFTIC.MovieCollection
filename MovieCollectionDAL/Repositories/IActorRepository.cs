using MovieCollectionDAL.Entities;
using System.Collections.Generic;

namespace MovieCollectionDAL.Repositories
{
    public interface IActorRepository
    {
        bool AddActorToMovie(int idArtist, int idMovie, string character);
        bool DeleteActorOfOneMovie_AllCharacters(int idArtist, int idMovie);
        bool DeleteActorOfOneMovie_OneCharacter(int idArtist, string character, int idMovie);
        bool DeleteActor_AllHisMovies(int idArtist);
        IEnumerable<Actor> GetAllActorsOfOneMovie(int idMovie);
        bool UpdateActorCharacter(int idArtist, int idMovie, string oldCharacter, string newCharacter);
        bool UpdateActorMovie(int idArtist, int oldIdMovie, string oldCharacter, int newIdMovie, string newCharacter);
    }
}