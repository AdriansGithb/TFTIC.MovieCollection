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
    public class MovieService : BaseService<Movie>, IMovieRepository
    {

        public MovieService(IConfiguration config) : base("Movie", config.GetConnectionString("default"))
        {
        }

        internal override Movie Converter(IDataRecord reader)
        {
            return new Movie
            {
                IdMovie = (int)reader["IdMovie"],
                Title = (string)reader["M_Title"],
                ReleaseYear = (int)reader["M_ReleaseYear"],
                Synopsys = (reader["M_Synopsys"] != DBNull.Value) ? (string)reader["M_Synopsys"] : "",
                TrailerLink = (reader["M_TrailerLink"] != DBNull.Value) ? (string)reader["M_TrailerLink"] : "",
                IsDeleted = (bool)reader["M_IsDeleted"],
                IdCountry = (reader["M_IdCountry"] != DBNull.Value) ? (int)reader["M_IdCountry"] : null,
                IdAudience = (reader["M_IdAudience"] != DBNull.Value) ? (int)reader["M_IdAudience"] : null
            };
        }

        public IEnumerable<Movie> GetAllNotDeleted()
        {
            Connection connection = new Connection(_connectionString);
            string Query = "SELECT * FROM [Movie] WHERE M_IsDeleted = 0";
            Command cmd = new Command(Query, false);

            return connection.ExecuteReader(cmd, Converter);
        }

        public bool Create(Movie m)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "INSERT INTO Movie (M_Title, M_ReleaseYear, M_Synopsys, M_TrailerLink,  M_IdCountry, M_IdAudience) VALUES (@title, @year, @synops, @trailer, @idcountry, @idaudience)";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("title", m.Title);
            cmd.AddParameter("year", m.ReleaseYear);
            cmd.AddParameter("synops", m.Synopsys);
            cmd.AddParameter("trailer", m.TrailerLink);
            cmd.AddParameter("idcountry", m.IdCountry);
            cmd.AddParameter("idaudience", m.IdAudience);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool Update(int idMovie, Movie m)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "UPDATE Movie SET M_Title = @title, M_ReleaseYear = @year, M_Synopsys = @synops, M_TrailerLink = @trailer, M_IdCountry = @idcountry, M_IdAudience = @idaudience WHERE IdMovie = @id";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("title", m.Title);
            cmd.AddParameter("year", m.ReleaseYear);
            cmd.AddParameter("synops", m.Synopsys);
            cmd.AddParameter("trailer", m.TrailerLink);
            cmd.AddParameter("idcountry", m.IdCountry);
            cmd.AddParameter("idaudience", m.IdAudience);
            cmd.AddParameter("id", idMovie);

            return connection.ExecuteNonQuery(cmd) == 1;
        }

        public override bool Delete(int Id)
        {
            Connection connection = new Connection(_connectionString);
            string Query = "UPDATE Movie SET M_IsDeleted = 1 WHERE IdMovie = @id";
            Command cmd = new Command(Query, false);
            cmd.AddParameter("id", Id);

            return connection.ExecuteNonQuery(cmd) == 1;
        }


    }

}

