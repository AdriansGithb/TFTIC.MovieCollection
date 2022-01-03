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

    }
}
