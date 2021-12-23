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
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genrRepo;

        public GenreController(IGenreRepository genrRepo)
        {
            _genrRepo = genrRepo;
        }

        /// <summary>
        /// Get all genres registered in db
        /// </summary>
        /// <returns>a IEnumerable containing object genres with id's, and labels</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_genrRepo.GetAll().Select(x => x.toWeb()));
        }
        /// <summary>
        /// Gets one genre by id
        /// </summary>
        /// <param name="Id">the unique genre id</param>
        /// <returns>an object containing the genre (id+label)</returns>
        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            return Ok(_genrRepo.GetById(Id).toWeb());
        }
        /// <summary>
        /// Registers a new genre in db
        /// </summary>
        /// <param name="Label">The name of the new genre</param>
        /// <returns>Ok or BadRequest</returns>
        [HttpPost]
        public IActionResult Create(string Label)
        {
            if (string.IsNullOrWhiteSpace(Label)) return BadRequest();
            if (!_genrRepo.Create(Label))
                return BadRequest("Erreur d'insertion");

            return Ok("Genre créé");
        }
        /// <summary>
        /// Updates the name of a genre
        /// </summary>
        /// <param name="Id">The id of the genre to update</param>
        /// <param name="newLabel">the new name of the genre</param>
        /// <returns>Ok if update succeeded or new genre created; BadRequest if error;</returns>
        [HttpPut("{Id}")]
        public IActionResult Update([FromRoute] int Id, [FromBody] string newLabel)
        {
            if (string.IsNullOrWhiteSpace(newLabel))
                return BadRequest();

            if (_genrRepo.GetById(Id) == null)
            {
                _genrRepo.Create(newLabel);
                return Ok("Genre créé");
            }

            if (!_genrRepo.Update(new Genre() { IdGenre = Id, Label = newLabel }.toDal()))
                return BadRequest("Mise à jour interrompue");

            return Ok("Update Ok");
        }
        /// <summary>
        /// Deletes a genre in db
        /// </summary>
        /// <param name="Id">The id of the genre to delete</param>
        /// <returns>OK if succeed</returns>
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _genrRepo.Delete(Id);
            return Ok();
        }


    }
}
