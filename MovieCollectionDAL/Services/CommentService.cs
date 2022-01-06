using Microsoft.Extensions.Configuration;
using MovieCollectionDAL.Entities;
using MovieCollectionDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace MovieCollectionDAL.Services
{
    public class CommentService : BaseService<Comment>, ICommentRepository
    {
        public CommentService(IConfiguration config) : base("Comment", config.GetConnectionString("default"))
        {
        }

        internal override Comment Converter(IDataRecord reader)
        {
            return new Comment
            {
                IdComment = (int)reader["IdComment"],
                Text = (string)reader["Com_Text"],
                IdMovie = (int)reader["Com_IdMovie"],
                CreatedBy = (Guid)reader["Com_CreatedBy"],
                CreationDate = (DateTime)reader["Com_CreationDate"],
                LastModifBy = (reader["Com_LastModifBy"] != DBNull.Value) ? (Guid)reader["Com_LastModifBy"] : null,
                LastModifDate = (reader["Com_LastModifDate"] != DBNull.Value) ? (DateTime)reader["Com_LastModifDate"] : null,
                DeletedBy = (reader["Com_DeletedBy"] != DBNull.Value) ? (Guid)reader["Com_DeletedBy"] : null,
                DeletionDate = (reader["Com_DeletionDate"] != DBNull.Value) ? (DateTime)reader["Com_DeletionDate"] : null
            };
        }

        public IEnumerable<Comment> GetAllUndeletedByMovie(int idMovie)
        {
            Connection connection = new Connection(_connectionString);
            string Query = "SELECT * FROM Comment WHERE Com_DeletedBy IS NULL AND Com_IdMovie = @idMovie";
            Command cmd = new Command(Query, false);

            cmd.AddParameter("idMovie", idMovie);

            return connection.ExecuteReader(cmd, Converter);
        }



        public bool Create(Comment c)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "INSERT INTO Comment (Com_Text, Com_IdMovie, Com_CreatedBy) VALUES (@txt, @idMovie, @idCreator)";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("txt", c.Text);
            cmd.AddParameter("idMovie", c.IdMovie);
            cmd.AddParameter("idCreator", c.CreatedBy);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool Update(Comment c)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "UPDATE Comment SET Com_Text = @txt, Com_IdMovie = @idMovie, Com_LastModifBy = @idEditor, Com_LastModifDate = @dateModif WHERE IdComment = @id";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("txt", c.Text);
            cmd.AddParameter("idMovie", c.IdMovie);
            cmd.AddParameter("idEditor", c.LastModifBy);
            cmd.AddParameter("dateModif", c.LastModifDate);
            cmd.AddParameter("id", c.IdComment);

            return connection.ExecuteNonQuery(cmd) == 1;
        }

        public bool Delete(Guid IdUser,int IdComment)
        {
            Connection connection = new Connection(_connectionString);
            string Query = "UPDATE Comment SET Com_DeletedBy = @idEraser, Com_DeletionDate = @delDate WHERE IdComment = @idCom";
            Command cmd = new Command(Query, false);
            cmd.AddParameter("idEraser", IdUser);
            cmd.AddParameter("delDate", DateTime.Now);
            cmd.AddParameter("idCom", IdComment);

            return connection.ExecuteNonQuery(cmd) == 1;
        }

        public override bool Delete(int IdComment)
        {
            Connection connection = new Connection(_connectionString);
            string Query = "UPDATE Comment SET Com_DeletedBy = @idEraser WHERE IdComment = @idCom";
            Command cmd = new Command(Query, false);
            cmd.AddParameter("idEraser", Guid.Empty);
            cmd.AddParameter("idCom", IdComment);

            return connection.ExecuteNonQuery(cmd) == 1;
        }


    }
}
