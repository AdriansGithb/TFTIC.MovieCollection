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
                BirthDate = (DateTime)reader["Art_BirthDate"]
            };
        }


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
        public bool Update(Artist a)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "UPDATE Artist SET Art_Name = @name, Art_FirstName = @fname, Art_BirthDate = @bdate WHERE IdArtist = @id";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("name", a.Name);
            cmd.AddParameter("fname", a.FirstName);
            cmd.AddParameter("bdate", a.BirthDate);
            cmd.AddParameter("id", a.IdArtist);

            return connection.ExecuteNonQuery(cmd) == 1;
        }

    }
}
