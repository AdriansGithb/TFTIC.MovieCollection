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
    public class AuthController : ControllerBase
    {
        private readonly IAppUserRepository _userRepo;
        private readonly TokenManager _tokenManager;

        public AuthController(IAppUserRepository userRepo, TokenManager tokenManager)
        {
            _userRepo = userRepo;
            _tokenManager = tokenManager;
        }
        /// <summary>
        /// Registers a new user in database
        /// </summary>
        /// <param name="form">a registerform with user's email, pass and name</param>
        /// <returns>Ok if succeeded, bad request if not</returns>
        [HttpPost("register")]
        public IActionResult Register(RegisterForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                if (!_userRepo.Register(form.toDal()))
                    return BadRequest("Erreur lors de l'enregistrement");
                return Ok("Utilisateur enregistré");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("login")]
        public IActionResult Login(LoginForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                User u = _userRepo.Login(form.Email, form.Password).toWeb();
                u.Token = _tokenManager.GenerateJWT(u);

                return Ok(u);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
