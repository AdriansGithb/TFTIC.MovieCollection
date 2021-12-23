using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionDAL.Entities
{
    public class Comment
    {
        public int IdComment { get; set; }
        public string Text { get; set; }
        public int IdMovie { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastModifBy { get; set; }
        public DateTime LastModifDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DeletionDate { get; set; }
    }
}
