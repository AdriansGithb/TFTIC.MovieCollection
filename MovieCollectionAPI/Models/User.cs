using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionAPI.Models
{
    public class User
    {
        public Guid IdUser { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
        //public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
