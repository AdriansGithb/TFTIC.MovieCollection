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
    [Authorize("user")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _cmntRepo;
        private readonly IMovieRepository _movieRepo;
        private readonly IAppUserRepository _userRepo;

        public CommentController(ICommentRepository cmntRepo, IMovieRepository movieRepo, IAppUserRepository userRepo)
        {
            _cmntRepo = cmntRepo;
            _movieRepo = movieRepo;
            _userRepo = userRepo;
        }


        /// <summary>
        /// Get all comments registered in db
        /// </summary>
        /// <returns>a IEnumerable containing comment objects</returns>
        [Authorize("Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_cmntRepo.GetAll().Select(x => x.toWeb(_movieRepo, _userRepo)));
        }
        /// <summary>
        /// Gets all undeleted comments by movie
        /// </summary>
        /// <param name="idMovie">the unique movie id</param>
        /// <returns>a IEnumerable containing the comments objects</returns>
        [HttpGet("/movie{idMovie}")]
        public IActionResult GetAllUndeletedByMovie(int idMovie)
        {
            return Ok(_cmntRepo.GetAllUndeletedByMovie(idMovie).Select(x => x.toWeb(_movieRepo, _userRepo)));
        }
        /// <summary>
        /// Gets one comment by id
        /// </summary>
        /// <param name="Id">the unique comment id</param>
        /// <returns>an object containing the comment</returns>
        [Authorize("Admin")]
        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            return Ok(_cmntRepo.GetById(Id).toWeb(_movieRepo, _userRepo));
        }
        /// <summary>
        /// Creates a new comment
        /// </summary>
        /// <param name="form">a comment form object of the new comment</param>
        /// <returns>Ok if succeeded, badrequest if not</returns>
        [HttpPost]
        public IActionResult Create(CommentForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (_userRepo.GetById(form.CreatedBy) == null)
                return BadRequest("L'utilisateur n'existe pas");
            if (!_cmntRepo.Create(form.toDal()))
                return BadRequest("Erreur d'insertion");

            return Ok("Commentaire créé");
        }
        /// <summary>
        /// Updates an existing comment
        /// </summary>
        /// <param name="Id">The id of the comment to update</param>
        /// <param name="form">the update form object of the comment</param>
        /// <returns>Ok if update succeeded ; BadRequest if error</returns>
        [Authorize("admin")]
        [HttpPut("modify")]
        public IActionResult Update([FromBody] CommentUpdateForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (_userRepo.GetById(form.ModifiedBy) == null)
                return BadRequest("L'utilisateur n'existe pas");

            if (!_cmntRepo.Update(form.toDal()))
                return BadRequest("Mise à jour interrompue");

            return Ok("Update Ok");
        }
        /// <summary>
        /// Sets a comment as deleted in db
        /// </summary>
        /// <param name="Id">the unique comment id</param>
        /// <param name="idAdmin">the admin's id</param>
        /// <returns>Ok if succeeded, bad request if not</returns>
        [Authorize("admin")]
        [HttpDelete("{Id}/{idAdmin}")]
        public IActionResult Delete(int Id, Guid idAdmin)
        {
            if (_userRepo.GetById(idAdmin) == null || !_userRepo.GetById(idAdmin).IsAdmin)
                return BadRequest("L'utilisateur n'existe pas ou n'est pas un administrateur");

            if (!_cmntRepo.Delete(idAdmin, Id))
                return BadRequest("Erreur lors de la suppression");
            return Ok("Suppression enregistrée");
        }

    }
}
