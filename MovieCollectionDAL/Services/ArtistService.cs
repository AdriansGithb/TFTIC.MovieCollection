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
    public class ArtistService : BaseService<Artist>,IArtistRepository
    {

        public ArtistService(IConfiguration config) : base("Artist", config.GetConnectionString("default"))
        {
        }

        internal override Artist Converter(IDataRecord reader)
        {
            return new Artist
            {
                IdArtist = (int)reader["IdArtist"],
                Name = (string)reader["Art_Name"],
                FirstName = (string)reader["Art_FirstName"],
                BirthDate = (reader["Art_BirthDate"] != DBNull.Value) ? (DateTime)reader["Art_BirthDate"] : null
            };
        }

        #region Artist
        public bool Create(Artist a)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "INSERT INTO Artist (Art_Name, Art_FirstName, Art_BirthDate) VALUES (@name, @fname, @bdate)";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("name", a.Name);
            cmd.AddParameter("fname", a.FirstName);
            cmd.AddParameter("bdate", a.BirthDate);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool Update(int idArtist,Artist a)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "UPDATE Artist SET Art_Name = @name, Art_FirstName = @fname, Art_BirthDate = @bdate WHERE IdArtist = @id";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("name", a.Name);
            cmd.AddParameter("fname", a.FirstName);
            cmd.AddParameter("bdate", a.BirthDate);
            cmd.AddParameter("id", idArtist);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        #endregion
        #region Producer
        //public bool AddToFilm(int idArtist, int idFilm, Enum role)
        //{

        //}
        public bool AddProducerToMovie(int idArtist, int idMovie)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "INSERT INTO Production VALUES (@idArtist, @idMovie)";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("idArtist", idArtist);
            cmd.AddParameter("idMovie", idMovie);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool DeleteProducerOfOneMovie(int idArtist, int idMovie)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "DELETE FROM Production WHERE P_IdArtist = @idArtist AND P_IdMovie = @idMovie";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("idArtist", idArtist);
            cmd.AddParameter("idMovie", idMovie);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool DeleteProducerOfAllMovies(int idArtist)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "DELETE FROM Production WHERE P_IdArtist = @idArtist";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("idArtist", idArtist);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public IEnumerable<Artist> GetAllProducers()
        {
            Connection connection = new Connection(_connectionString);
            string sql = "SELECT * FROM Artist " +
                "WHERE IdArtist IN " +
                "(SELECT DISTINCT P_IdArtist " +
                "FROM Production P JOIN Movie M ON P.P_IdMovie = M.IdMovie " +
                "WHERE M.M_IsDeleted = 0)";
            Command cmd = new Command(sql, false);

            return connection.ExecuteReader(cmd, Converter);
        }
        public IEnumerable<Artist> GetAllProducersOfOneMovie(int idMovie)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "SELECT * FROM Artist " +
                "WHERE IdArtist IN " +
                "(SELECT P_IdArtist " +
                "FROM Production P JOIN Movie M ON P.P_IdMovie = M.IdMovie " +
                "WHERE M.M_IsDeleted = 0 AND P.P_IdMovie = @idMovie )";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("idMovie", idMovie);

            return connection.ExecuteReader(cmd, Converter);
        }

        #endregion
        #region Director
        public bool AddDirectorToMovie(int idArtist, int idMovie)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "INSERT INTO Direction VALUES (@idArtist, @idMovie)";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("idArtist", idArtist);
            cmd.AddParameter("idMovie", idMovie);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool DeleteDirectorOfOneMovie(int idArtist, int idMovie)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "DELETE FROM Direction WHERE D_IdArtist = @idArtist AND D_IdMovie = @idMovie";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("idArtist", idArtist);
            cmd.AddParameter("idMovie", idMovie);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool DeleteDirectorOfAllMovies(int idArtist)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "DELETE FROM Direction WHERE P_IdArtist = @idArtist";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("idArtist", idArtist);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public IEnumerable<Artist> GetAllDirectors()
        {
            Connection connection = new Connection(_connectionString);
            string sql = "SELECT * FROM Artist " +
                "WHERE IdArtist IN " +
                "(SELECT DISTINCT D_IdArtist " +
                "FROM Direction D JOIN Movie M ON D.D_IdMovie = M.IdMovie " +
                "WHERE M.M_IsDeleted = 0)";
            Command cmd = new Command(sql, false);

            return connection.ExecuteReader(cmd, Converter);
        }
        public IEnumerable<Artist> GetAllDirectorsOfOneMovie(int idMovie)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "SELECT * FROM Artist " +
                "WHERE IdArtist IN " +
                "(SELECT D_IdArtist " +
                "FROM Direction D JOIN Movie M ON D.D_IdMovie = M.IdMovie " +
                "WHERE M.M_IsDeleted = 0 AND D.D_IdMovie = @idMovie )";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("idMovie", idMovie);

            return connection.ExecuteReader(cmd, Converter);
        }

        #endregion
        #region Actor
        public IEnumerable<Artist> GetAllActors()
        {
            Connection connection = new Connection(_connectionString);
            string sql = "SELECT * FROM Artist " +
                "WHERE IdArtist IN " +
                "(SELECT DISTINCT Act_IdArtist " +
                "FROM Acting A JOIN Movie M ON A.Act_IdMovie = M.IdMovie " +
                "WHERE M.M_IsDeleted = 0)";
            Command cmd = new Command(sql, false);

            return connection.ExecuteReader(cmd, Converter);
        }

        #endregion

    }
}
