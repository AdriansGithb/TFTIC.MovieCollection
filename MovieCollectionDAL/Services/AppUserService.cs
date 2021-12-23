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
    public class AppUserService : BaseService<AppUser>, IAppUserRepository
    {
        public AppUserService(IConfiguration config) : base("AppUser", config.GetConnectionString("default"))
        {
        }

        internal override AppUser Converter(IDataRecord reader)
        {
            return new AppUser
            {
                IdUser = (string)reader["IdUser"],
                Email = (string)reader["U_Email"],
                Password = (string)reader["U_Password"],
                Name = (string)reader["U_Name"],
                IsAdmin = (bool)reader["U_IsAdmin"],
                CreationDate = (DateTime)reader["U_CreationDate"],
                IsDeleted = (bool)reader["U_IsDeleted"]
            };
        }


        public bool Create(AppUser u)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "INSERT INTO AppUser (U_Email, U_Password, U_Name, U_IsAdmin) VALUES (@mail, @pass, @name, @isadmin)";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("mail", u.Email);
            cmd.AddParameter("pass", u.Password);
            cmd.AddParameter("name", u.Name);
            cmd.AddParameter("isadmin", u.IsAdmin);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool Update(AppUser u)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "UPDATE AppUser SET U_Email = @mail, U_Password = @pass, U_Name = @name, U_IsAdmin = @isadmin WHERE IdUser = @id";
            Command cmd = new Command(sql, false);

            cmd.AddParameter("mail", u.Email);
            cmd.AddParameter("pass", u.Password);
            cmd.AddParameter("name", u.Name);
            cmd.AddParameter("isadmin", u.IsAdmin);
            cmd.AddParameter("id", u.IdUser);

            return connection.ExecuteNonQuery(cmd) == 1;
        }

        public override bool Delete(int Id)
        {
            Connection connection = new Connection(_connectionString);
            string Query = "UPDATE AppUSer SET U_IsDeleted = 1 WHERE IdUser = @id";
            Command cmd = new Command(Query, false);
            cmd.AddParameter("id", Id);

            return connection.ExecuteNonQuery(cmd) == 1;
        }

    }
}
