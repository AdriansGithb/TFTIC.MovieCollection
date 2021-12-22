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
    public class GenreService : BaseService<Genre>, IGenreRepository
    {
        public GenreService(IConfiguration config) : base("Genre", config.GetConnectionString("default"))
        {
        }

        internal override Genre Converter(IDataRecord reader)
        {
            return new Genre
            {
                IdGenre = (int)reader["IdGenre"],
                Label = (string)reader["G_Label"]
            };
        }


        public bool Create(Genre g)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "INSERT INTO Genre (G_Label) VALUES (@label)";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("label", g.Label);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool Update(Genre g)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "UPDATE Genre SET G_Label = @label WHERE IdGenre = @id";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("label", g.Label);
            cmd.AddParameter("id", g.IdGenre);

            return connection.ExecuteNonQuery(cmd) == 1;
        }

    }
}
