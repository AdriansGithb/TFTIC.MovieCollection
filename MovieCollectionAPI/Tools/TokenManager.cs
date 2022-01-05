using Microsoft.IdentityModel.Tokens;
using MovieCollectionAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionAPI.Tools
{
    public class TokenManager
    {
        public static string secretKey = "Quand on l'attaque l'Emp!re Contre-Attqu3 !!";
        public static string issuer = "monSiteAPI.com"; // l'adresse du site API
        public static string audience = "monSiteCONSO.com";  // l'adresse du site de consommation de l'API

        public string GenerateJWT(User connectedUser)
        {
            if (string.IsNullOrWhiteSpace(connectedUser.Email))
            {
                throw new ArgumentNullException();
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Email, connectedUser.Email),
                new Claim(ClaimTypes.Role, connectedUser.IsAdmin ? "admin" : "user"),
                new Claim("UserId", connectedUser.IdUser.ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                issuer: issuer,
                audience: audience,
                expires: DateTime.Now.AddMinutes(60)
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }
    }
}
