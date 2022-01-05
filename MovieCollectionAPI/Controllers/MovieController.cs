using Microsoft.AspNetCore.Authorization;
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
    [Authorize("user")]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepo;
        private readonly IAudienceRepository _audienceRepo;
        private readonly IArtistRepository _artistRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly ICountryRepository _countryRepo;

        public MovieController(IMovieRepository movieRepo, IAudienceRepository audienceRepo, IArtistRepository artistRepo, IGenreRepository genreRepo, ICountryRepository countryRepo)
        {
            _movieRepo = movieRepo;
            _audienceRepo = audienceRepo;
            _artistRepo = artistRepo;
            _genreRepo = genreRepo;
            _countryRepo = countryRepo;
        }

        /// <summary>
        /// Get all movies registered in db
        /// </summary>
        /// <returns>a IEnumerable containing movies objects with details, origin country, audience, producers, realisators, actors</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_movieRepo.GetAllNotDeleted().Select(x => x.toWeb(_countryRepo, _audienceRepo, _genreRepo)));
        }
        /// <summary>
        /// Gets one movie by id
        /// </summary>
        /// <param name="Id">the unique movie id</param>
        /// <returns>an object containing the movie</returns>
        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            return Ok(_movieRepo.GetById(Id).toWeb(_countryRepo, _audienceRepo, _genreRepo));
        }
        /// <summary>
        /// Creates a new movie
        /// </summary>
        /// <param name="form">a movie form object of the new movie</param>
        /// <returns>Ok if succeeded, badrequest if not</returns>
        [Authorize("admin")]
        [HttpPost]
        public IActionResult Create(MovieForm form)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (!_movieRepo.Create(form.toDal())) return BadRequest("Erreur d'insertion");

            return Ok("Film créé");
        }
        /// <summary>
        /// Updates an existing movie
        /// </summary>
        /// <param name="Id">The id of the movie to update</param>
        /// <param name="form">the form object of the movie</param>
        /// <returns>Ok if update succeeded or new movie created; BadRequest if error;</returns>
        [Authorize("admin")]
        [HttpPut("{Id}")]
        public IActionResult Update([FromRoute] int Id, [FromBody] MovieForm form)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (_movieRepo.GetById(Id) == null)
            {
                _movieRepo.Create(form.toDal());
                return Ok("Film créé");
            }

            if (!_movieRepo.Update(Id, form.toDal()))
                return BadRequest("Mise à jour interrompue");

            return Ok("Update Ok");
        }
        /// <summary>
        /// Sets a movie as deleted in db
        /// </summary>
        /// <param name="Id">The id of the movie to delete</param>
        /// <returns>OK if succeed</returns>
        [Authorize("admin")]
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _movieRepo.Delete(Id);
            return Ok();
        }

    }
}
