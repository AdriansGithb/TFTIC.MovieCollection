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
    public class UserController : ControllerBase
    {
        private readonly IAppUserRepository _userRepo;

        public UserController(IAppUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        /// <summary>
        /// Get all users registered in db
        /// </summary>
        /// <returns>a IEnumerable containing object users</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userRepo.GetAll().Select(x => x.toWeb()));
        }
        /// <summary>
        /// Gets one user by id
        /// </summary>
        /// <param name="Id">the unique user id</param>
        /// <returns>an object containing the user</returns>
        [HttpGet("{Id}")]
        public IActionResult GetById([FromRoute]Guid Id)
        {
            return Ok(_userRepo.GetById(Id).toWeb());
        }
        /// <summary>
        /// Updates email's and or name's user
        /// </summary>
        /// <param name="id">the unique user's id</param>
        /// <param name="form">an object containing the new details to update</param>
        /// <returns>Ok if succeed, bad request if not</returns>
        [HttpPut("updateDetails/{id}")]
        public IActionResult UpdateUserDetails([FromRoute] Guid id, [FromBody] UserUpdateForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_userRepo.GetById(id) == null)
            {
                return BadRequest("Id utilisateur inexistant, créez un nouvel utilisateur ou contactez un administrateur");
            }

            if (!_userRepo.Update(form.toDal(id)))
                return BadRequest("Mise à jour interrompue");

            return Ok("Update Ok");
        }
        /// <summary>
        /// Updates password's user
        /// </summary>
        /// <param name="id">the unique user's id</param>
        /// <param name="form">an object containing the new password to update</param>
        /// <returns>Ok if succeed, bad request if not</returns>
        [HttpPut("updatePassword/{id}")]
        public IActionResult UpdateUserPassword([FromRoute] Guid id, [FromBody] UpdatePassForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (form.ActualPassword == form.NewPassword)
                return BadRequest("Le nouveau mot de passe doit différer de l'ancien");

            if (_userRepo.GetById(id) == null)
            {
                return BadRequest("Id utilisateur inexistant");
            }

            if (!_userRepo.UpdatePassword(id, form.ActualPassword, form.NewPassword))
                return BadRequest("Mise à jour interrompue");

            return Ok("Update Ok");
        }
        /// <summary>
        /// Undeletes a deleted user in db
        /// </summary>
        /// <param name="id">the unique user's id</param>
        /// <returns>OK if succeed, bad request if not</returns>
        [HttpPut("undelete/{id}")]
        public IActionResult Undelete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_userRepo.GetById(id) == null)
            {
                return BadRequest("Id utilisateur inexistant");
            }

            if (!_userRepo.Undelete(id))
                return BadRequest("Mise à jour interrompue");

            return Ok("Update Ok");
        }


        /// <summary>
        /// Sets a user as deleted in db
        /// </summary>
        /// <param name="Id">the user's id to set as deleted</param>
        /// <returns>OK if succeed</returns>
        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            if(_userRepo.Delete(Id))
                return Ok("Suppression de l'utilisateur enregistrée");
            return BadRequest("Erreur lors de la suppression");
        }

    }
}
