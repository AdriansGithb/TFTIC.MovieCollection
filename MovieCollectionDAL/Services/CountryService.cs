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
    public class CountryService : BaseService<Country>, ICountryRepository
    {
        public CountryService(IConfiguration config) : base("Country", config.GetConnectionString("default"))
        {
        }

        internal override Country Converter(IDataRecord reader)
        {
            return new Country
            {
                IdCountry = (int)reader["IdCountry"],
                Name = (string)reader["C_Name"]
            };
        }


        public bool Create(Country c)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "INSERT INTO Country (C_Name) VALUES (@name)";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("name", c.Name);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool Update(Country c)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "UPDATE Country SET C_Name = @name WHERE IdCountry = @id";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("name", c.Name);
            cmd.AddParameter("id", c.IdCountry);

            return connection.ExecuteNonQuery(cmd) == 1;
        }

    }

}

