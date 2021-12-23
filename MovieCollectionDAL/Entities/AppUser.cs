using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionDAL.Entities
{
    public class AppUser
    {
        public string IdUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
