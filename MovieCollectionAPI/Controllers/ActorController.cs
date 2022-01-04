using Microsoft.AspNetCore.Mvc;
using MovieCollectionAPI.Models;
using MovieCollectionAPI.Tools;
using MovieCollectionDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorRepository _actRepo;
        private readonly IArtistRepository _artRepo;

        public ActorController(IActorRepository actRepo, IArtistRepository artRepo)
        {
            _actRepo = actRepo;
            _artRepo = artRepo;
        }


        /// <summary>
        /// Get all actors registered in db
        /// </summary>
        /// <returns>a IEnumerable containing artist objects with name, firstname and birthdate</returns>
        [HttpGet]
        public IActionResult GetAllActors()
        {
            return RedirectToAction("GetAllActors", "Artist");
        }
        /// <summary>
        /// Gets all actors of a movie
        /// </summary>
        /// <param name="IdMovie">the unique movie id</param>
        /// <returns>a IEnumerable containing actors objects with name, firstname and birthdate, and their characters</returns>
        [HttpGet("/movie/{IdMovie}")]
        public IActionResult GetActorsByMovie(int IdMovie)
        {
            return Ok(_actRepo.GetAllActorsOfOneMovie(IdMovie).Select(x => x.toWeb(_artRepo)));
        }
        /// <summary>
        /// Adds an actor to a movie
        /// </summary>
        /// <param name="idActor">the unique id of the actor to add</param>
        /// <param name="idMovie">the unique id of the movie wherein add the actor</param>
        /// <param name="character">the name of his character in the movie</param>
        /// <returns>Ok if succeeded, bad request if not</returns>        
        [HttpPost("/AddToMovie")]
        public IActionResult AddActorToMovie(ActorForm a)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (!_actRepo.AddActorToMovie(a.IdArtist, a.IdMovie, a.Character)) return BadRequest("Erreur d'insertion");

            return Ok("Acteur ajouté");
        }
        /// <summary>
        /// Updates an actor's character in a movie
        /// </summary>
        /// <param name="idActor">the unique id of the actor</param>
        /// <param name="idMovie">the unique id of the movie</param>
        /// <param name="oldCharacter">the old name of his character in the movie</param>
        /// <param name="newCharacter">the new name of his character in the movie</param>
        /// <returns>Ok if succeeded, bad request if not</returns>        
        [HttpPut]
        public IActionResult UpdateActorCharacter(ActorForm newCharacter, string oldCharacter)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (!_actRepo.UpdateActorCharacter(newCharacter.IdArtist, newCharacter.IdMovie, oldCharacter, newCharacter.Character)) return BadRequest("Erreur de mise à jour");

            return Ok("Personnage mis à jour");
        }
        /// <summary>
        /// Deletes all links between an actor and any movies
        /// </summary>
        /// <param name="Id">the unique id of the actor to unlink</param>
        /// <returns>Ok if succeeded, bad request if not</returns>
        [HttpDelete("Id")]
        public IActionResult DeleteAllMoviesOfAnActor(int Id)
        {
            if (!_actRepo.DeleteActor_AllHisMovies(Id))
                return BadRequest("Suppression impossible");
            return Ok("Activité de l'acteur supprimée");
        }
        /// <summary>
        ///Deletes a character of an actor from one movie
        /// </summary>
        /// <param name="a">the actor object containing the character and the movie</param>
        /// <returns>Ok if succeeded, bad request if not</returns>
        [HttpDelete("/movie/deleteCharacter")]
        public IActionResult DeleteCharacterFromOneMovie(ActorForm a)
        {
            if (!_actRepo.DeleteActorOfOneMovie_OneCharacter(a.IdArtist, a.Character, a.IdMovie))
                return BadRequest("Suppression impossible");
            return Ok("Personnage supprimé");
        }
        /// <summary>
        /// Deletes all character of an actor from one movie
        /// </summary>
        /// <param name="idActor">the unique id of the actor</param>
        /// <param name="idMovie">the unique id of the movie</param>
        /// <returns>Ok if succeeded, bad request if not</returns>
        [HttpDelete("/movie{idMovie}/deleteAllCharacters{idActor}")]
        public IActionResult DeleteAllCharactersOfOneActorFromOneMovie([FromRoute]int idActor, [FromRoute]int idMovie)
        {
            if (!_actRepo.DeleteActorOfOneMovie_AllCharacters(idActor, idMovie))
                return BadRequest("Suppression impossible");
            return Ok("Personnages supprimés");
        }

    }
}
