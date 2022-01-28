using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _cntryRepo;

        public CountryController(ICountryRepository cntryRepo)
        {
            _cntryRepo = cntryRepo;
        }

        /// <summary>
        /// Get all countries registered in db
        /// </summary>
        /// <returns>a IEnumerable containing object countries with id's, and names</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_cntryRepo.GetAll().Select(x => x.toWeb()));
        }
        /// <summary>
        /// Gets one country by id
        /// </summary>
        /// <param name="Id">the unique country id</param>
        /// <returns>an object containing the country (id+name)</returns>
        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            return Ok(_cntryRepo.GetById(Id).toWeb());
        }
        /// <summary>
        /// Registers a new country in db
        /// </summary>
        /// <param name="form">The name of the new country</param>
        /// <returns>Ok or BadRequest</returns>
        [HttpPost]
        public IActionResult Create(CountryCreationForm form)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (!_cntryRepo.Create(form.Name)) return BadRequest("Erreur d'insertion");

            return Ok();
        }
        /// <summary>
        /// Updates the name of a country
        /// </summary>
        /// <param name="Id">The id of the country to update</param>
        /// <param name="newName">the new name of the country</param>
        /// <returns>Ok if update succeeded or new country created; BadRequest if error;</returns>
        [HttpPut]
        public IActionResult Update(CountryUpdateForm form)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (_cntryRepo.GetById(form.IdCountry) == null)
            {
                _cntryRepo.Create(form.NewName);
                return Ok("Pays créé");
            }

            if (!_cntryRepo.Update(new Country(){IdCountry = form.IdCountry, Name = form.NewName }.toDal())) 
                return BadRequest("Mise à jour interrompue");

            return Ok();
        }
        /// <summary>
        /// Deletes a country in db
        /// </summary>
        /// <param name="Id">The id of the country to delete</param>
        /// <returns>OK if succeed</returns>
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _cntryRepo.Delete(Id);
            return Ok();
        }


    }
}
