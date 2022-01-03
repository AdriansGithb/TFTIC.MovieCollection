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


        public bool Create(string label)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "INSERT INTO Genre (G_Label) VALUES (@label)";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("label", label);

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

        public IEnumerable<Genre> GetByFilmId(int IdFilm)
        {
            Connection connection = new Connection(_connectionString);
            string Query = "SELECT * FROM Genre WHERE IdGenre IN(SELECT MG_IdGenre FROM Movie_Genre WHERE MG_IdMovie = @idFilm)";
            Command cmd = new Command(Query, false);
            cmd.AddParameter("IdFilm", IdFilm);

            return connection.ExecuteReader(cmd, Converter);
        }
    }
}
