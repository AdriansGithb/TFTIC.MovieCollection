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
    public class AudienceService : BaseService<Audience>, IAudienceRepository
    {
        public AudienceService(IConfiguration config) : base("Audience", config.GetConnectionString("default"))
        {
        }

        internal override Audience Converter(IDataRecord reader)
        {
            return new Audience
            {
                IdAudience = (int)reader["IdAudience"],
                Label = (string)reader["Au_Label"]
            };
        }


        public bool Create(Audience a)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "INSERT INTO Audience (Au_Label) VALUES (@label)";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("label", a.Label);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool Update(Audience a)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "UPDATE Audience SET Au_Label = @label WHERE IdAudience = @id";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("label", a.Label);
            cmd.AddParameter("id", a.IdAudience);

            return connection.ExecuteNonQuery(cmd) == 1;
        }

    }
}
