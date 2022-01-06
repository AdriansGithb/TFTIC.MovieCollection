using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionAPI.Models
{
    public class Comment
    {
        public int IdComment { get; set; }
        public string Text { get; set; }
        public Movie Movie { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public User LastModifBy { get; set; }
        public DateTime? LastModifDate { get; set; }
        public User DeletedBy { get; set; }
        public DateTime? DeletionDate { get; set; }

    }
}
