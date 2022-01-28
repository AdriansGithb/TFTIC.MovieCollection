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
    [Authorize("admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AudienceController : ControllerBase
    {
        private readonly IAudienceRepository _audRepo;

        public AudienceController(IAudienceRepository audRepo)
        {
            _audRepo = audRepo;
        }

        /// <summary>
        /// Get all audiences registered in db
        /// </summary>
        /// <returns>a IEnumerable containing object audiences with id's, and labels</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_audRepo.GetAll().Select(x => x.toWeb()));
        }
        /// <summary>
        /// Gets one audience by id
        /// </summary>
        /// <param name="Id">the unique audience id</param>
        /// <returns>an object containing the audience (id+label)</returns>
        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            return Ok(_audRepo.GetById(Id).toWeb());
        }
        /// <summary>
        /// Registers a new audience in db
        /// </summary>
        /// <param name="form">The name of the new audience</param>
        /// <returns>Ok or BadRequest</returns>
        [HttpPost]
        public IActionResult Create(AudienceCreationForm form)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (!_audRepo.Create(form.Label))
                return BadRequest("Erreur d'insertion");

            return Ok();
        }
        /// <summary>
        /// Updates the name of an audience
        /// </summary>
        /// <param name="Id">The id of the audience to update</param>
        /// <param name="newLabel">the new name of the audience</param>
        /// <returns>Ok if update succeeded or new audience created; BadRequest if error;</returns>
        [HttpPut]
        public IActionResult Update(AudienceUpdateForm form)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (_audRepo.GetById(form.IdAudience) == null)
            {
                _audRepo.Create(form.NewLabel);
                return Ok();
            }

            if (!_audRepo.Update(new Audience() { IdAudience = form.IdAudience, Label = form.NewLabel }.toDal()))
                return BadRequest("Mise à jour interrompue");

            return Ok();
        }
        /// <summary>
        /// Deletes an audience in db
        /// </summary>
        /// <param name="Id">The id of the audience to delete</param>
        /// <returns>OK if succeed</returns>
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _audRepo.Delete(Id);
            return Ok();
        }
    }
}
