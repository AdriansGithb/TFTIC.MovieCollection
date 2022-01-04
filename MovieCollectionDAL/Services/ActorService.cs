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
    public class ActorService : IActorRepository
    {
        protected string _connectionString;

        public ActorService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default");
        }

        public bool AddActorToMovie(int idArtist, int idMovie, string character)
        {
            if (string.IsNullOrWhiteSpace(character))
                return false;
            Connection connection = new Connection(_connectionString);
            string sql = "INSERT INTO Acting VALUES (@idArtist, @idMovie, @character)";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("idArtist", idArtist);
            cmd.AddParameter("idMovie", idMovie);
            cmd.AddParameter("character", character);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool UpdateActorMovie(int idArtist, int oldIdMovie, string oldCharacter, int newIdMovie, string newCharacter)
        {
            if (string.IsNullOrWhiteSpace(oldCharacter) || string.IsNullOrWhiteSpace(newCharacter))
                return false;
            Connection connection = new Connection(_connectionString);
            string sql = "UPDATE Acting SET Act_IdMovie = @newIdMovie, Act_Character = @newCharacter WHERE Act_IdArtist = @idArtist AND Act_IdMovie = oldIdMovie AND Act_Character = @oldCharacter";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("idArtist", idArtist);
            cmd.AddParameter("oldIdMovie", oldIdMovie);
            cmd.AddParameter("newIdMovie", newIdMovie);
            cmd.AddParameter("oldCharacter", oldCharacter);
            cmd.AddParameter("newCharacter", newCharacter);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool UpdateActorCharacter(int idArtist, int idMovie, string oldCharacter, string newCharacter)
        {
            if (string.IsNullOrWhiteSpace(oldCharacter) || string.IsNullOrWhiteSpace(newCharacter))
                return false;
            Connection connection = new Connection(_connectionString);
            string sql = "UPDATE Acting SET Act_Character = @newCharacter WHERE Act_IdArtist = @idArtist AND Act_IdMovie = idMovie AND Act_Character = @oldCharacter";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("idArtist", idArtist);
            cmd.AddParameter("idMovie", idMovie);
            cmd.AddParameter("oldCharacter", oldCharacter);
            cmd.AddParameter("newCharacter", newCharacter);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool DeleteActorOfOneMovie_AllCharacters(int idArtist, int idMovie)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "DELETE FROM Acting WHERE Act_IdArtist = @idArtist AND Act_IdMovie = @idMovie";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("idArtist", idArtist);
            cmd.AddParameter("idMovie", idMovie);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool DeleteActorOfOneMovie_OneCharacter(int idArtist, string character, int idMovie)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "DELETE FROM Acting WHERE Act_IdArtist = @idArtist AND Act_IdMovie = @idMovie AND Act_Character = @character";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("idArtist", idArtist);
            cmd.AddParameter("character", character);
            cmd.AddParameter("idMovie", idMovie);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool DeleteActor_AllHisMovies(int idArtist)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "DELETE FROM Acting WHERE Act_IdArtist = @idArtist";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("idArtist", idArtist);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public IEnumerable<Actor> GetAllActorsOfOneMovie(int idMovie)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "SELECT * FROM Acting WHERE Act_IdMovie = @idMovie";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("idMovie", idMovie);

            return connection.ExecuteReader(cmd, Converter);
        }
        internal Actor Converter(IDataRecord reader)
        {
            return new Actor
            {
                IdArtist = (int)reader["Act_IdArtist"],
                IdMovie = (int)reader["Act_IdMovie"],
                Character = (string)reader["Act_Character"]
            };
        }

    }
}
