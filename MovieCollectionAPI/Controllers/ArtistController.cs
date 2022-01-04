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
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artRepo;

        public ArtistController(IArtistRepository artRepo)
        {
            _artRepo = artRepo;
        }
        #region Artist
        /// <summary>
        /// Get all artists registered in db
        /// </summary>
        /// <returns>a IEnumerable containing artist objects with name, firstname and birthdate</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_artRepo.GetAll().Select(x => x.toWeb()));
        }
        /// <summary>
        /// Gets one artist by id
        /// </summary>
        /// <param name="Id">the unique artist id</param>
        /// <returns>an object containing the artist</returns>
        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            return Ok(_artRepo.GetById(Id).toWeb());
        }
        /// <summary>
        /// Creates a new artist
        /// </summary>
        /// <param name="form">a form object of the new artist</param>
        /// <returns>Ok if succeeded, badrequest if not</returns>
        [HttpPost]
        public IActionResult Create(ArtistForm form)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (!_artRepo.Create(form.toDal())) return BadRequest("Erreur d'insertion");

            return Ok("Artiste créé");
        }
        /// <summary>
        /// Updates an existing artist
        /// </summary>
        /// <param name="Id">The id of the artist to update</param>
        /// <param name="form">the form object of the artist</param>
        /// <returns>Ok if update succeeded or new artist created; BadRequest if error;</returns>
        [HttpPut("{Id}")]
        public IActionResult Update([FromRoute] int Id, [FromBody] ArtistForm form)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (_artRepo.GetById(Id) == null)
            {
                _artRepo.Create(form.toDal());
                return Ok("Artiste créé");
            }

            if (!_artRepo.Update(Id, form.toDal()))
                return BadRequest("Mise à jour interrompue");

            return Ok("Update Ok");
        }
        /// <summary>
        /// Deltes an artist in db
        /// </summary>
        /// <param name="Id">The id of the artist to delete</param>
        /// <returns>OK if succeed</returns>
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _artRepo.Delete(Id);
            return Ok();
        }
        #endregion
        #region Producer
        /// <summary>
        /// Get all producers registered in db
        /// </summary>
        /// <returns>a IEnumerable containing artist objects with name, firstname and birthdate</returns>
        [HttpGet("/producers")]
        public IActionResult GetAllProducers()
        {
            return Ok(_artRepo.GetAllProducers().Select(x => x.toWeb()));
        }
        /// <summary>
        /// Gets all producers of a movie
        /// </summary>
        /// <param name="IdMovie">the unique movie id</param>
        /// <returns>a IEnumerable containing artist objects with name, firstname and birthdate</returns>
        [HttpGet("/producer/{IdMovie}")]
        public IActionResult GetProducersByMovie(int IdMovie)
        {
            return Ok(_artRepo.GetAllProducersOfOneMovie(IdMovie).Select(x => x.toWeb()));
        }
        /// <summary>
        /// Adds a producer to a movie
        /// </summary>
        /// <param name="idProducer">the unique id of the producer to add</param>
        /// <param name="idMovie">the unique id of the movie wherein add the producer</param>
        /// <returns>Ok if succeeded, bad request if not</returns>        
        [HttpPost("/producer/{idProducer}")]
        public IActionResult AddProducerToMovie([FromRoute]int idProducer,[FromBody]int idMovie)
        {
            if (!_artRepo.AddProducerToMovie(idProducer, idMovie)) return BadRequest("Erreur d'insertion");

            return Ok("Producteur ajouté");
        }
        /// <summary>
        /// Deletes a producer from one movie
        /// </summary>
        /// <param name="idProducer">the unique id of the producer to remove</param>
        /// <param name="idMovie">the unique id of the movie from which delete the producer</param>
        /// <returns>Ok if succeeded</returns>
        [HttpDelete("/producer/deleteOne/{idProducer}")]
        public IActionResult DeleteProducerFromOneMovie([FromRoute] int idProducer, [FromBody] int idMovie)
        {
            if(!_artRepo.DeleteProducerOfOneMovie(idProducer, idMovie))
                return BadRequest("Suppression impossible");
            return Ok("Producteur supprimé");
        }
        /// <summary>
        /// Deletes a producer from all movies
        /// </summary>
        /// <param name="idProducer">the unique id of the producer to remove</param>
        /// <returns>Ok if succeeded, bad request if not</returns>
        [HttpDelete("/producer/deleteAll/{idProducer}")]
        public IActionResult DeleteProducerAllMovies(int idProducer)
        {
            if(!_artRepo.DeleteProducer_AllHisMovies(idProducer))
                return BadRequest("Suppression impossible");
            return Ok("Producteur supprimé");
        }

        #endregion
        #region Director
        /// <summary>
        /// Get all directors registered in db
        /// </summary>
        /// <returns>a IEnumerable containing artist objects with name, firstname and birthdate</returns>
        [HttpGet("/directors")]
        public IActionResult GetAllDirectors()
        {
            return Ok(_artRepo.GetAllDirectors().Select(x => x.toWeb()));
        }
        /// <summary>
        /// Gets all directors of a movie
        /// </summary>
        /// <param name="IdMovie">the unique movie id</param>
        /// <returns>a IEnumerable containing artist objects with name, firstname and birthdate</returns>
        [HttpGet("/director/{IdMovie}")]
        public IActionResult GetDirectorsByMovie(int IdMovie)
        {
            return Ok(_artRepo.GetAllDirectorsOfOneMovie(IdMovie).Select(x => x.toWeb()));
        }
        /// <summary>
        /// Adds a director to a movie
        /// </summary>
        /// <param name="idDirector">the unique id of the director to add</param>
        /// <param name="idMovie">the unique id of the movie wherein add the director</param>
        /// <returns>Ok if succeeded, bad request if not</returns>        
        [HttpPost("/director/{idDirector}")]
        public IActionResult AddDirectorToMovie([FromRoute] int idDirector, [FromBody] int idMovie)
        {
            if (!_artRepo.AddDirectorToMovie(idDirector, idMovie)) return BadRequest("Erreur d'insertion");

            return Ok("Réalisateur ajouté");
        }
        /// <summary>
        /// Deletes a director from one movie
        /// </summary>
        /// <param name="idDirector">the unique id of the director to remove</param>
        /// <param name="idMovie">the unique id of the movie from which delete the director</param>
        /// <returns>Ok if succeeded</returns>
        [HttpDelete("/director/deleteOne/{idDirector}")]
        public IActionResult DeleteDirectorFromOneMovie([FromRoute] int idDirector, [FromBody] int idMovie)
        {
            if (!_artRepo.DeleteDirectorOfOneMovie(idDirector, idMovie))
                return BadRequest("Suppression impossible");
            return Ok("Réalisateur supprimé");
        }
        /// <summary>
        /// Deletes a director from all his movies
        /// </summary>
        /// <param name="idDirector">the unique id of the director to remove</param>
        /// <returns>Ok if succeeded, bad request if not</returns>
        [HttpDelete("/director/deleteAll/{idDirector}")]
        public IActionResult DeleteDirectorAllMovies(int idDirector)
        {
            if (!_artRepo.DeleteDirector_AllHisMovies(idDirector))
                return BadRequest("Suppression impossible");
            return Ok("Réalisateur supprimé");
        }
        #endregion
        #region Actor
        /// <summary>
        /// Get all actors registered in db
        /// </summary>
        /// <returns>a IEnumerable containing artist objects with name, firstname and birthdate</returns>
        [HttpGet("/actors")]
        public IActionResult GetAllActors()
        {
            return Ok(_artRepo.GetAllActors().Select(x => x.toWeb()));
        }

        #endregion
    }
}
