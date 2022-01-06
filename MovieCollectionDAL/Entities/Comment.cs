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
        public Guid CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid? LastModifBy { get; set; }
        public DateTime? LastModifDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletionDate { get; set; }
    }
}
