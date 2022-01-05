using Microsoft.Extensions.Configuration;
using MovieCollectionDAL.Entities;
using MovieCollectionDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                IdUser = (Guid)reader["IdUser"],
                Email = (string)reader["U_Email"],
                //Password = (string)reader["U_Password"],
                Name = (string)reader["U_Name"],
                IsAdmin = (bool)reader["U_IsAdmin"],
                CreationDate = (DateTime)reader["U_CreationDate"],
                IsDeleted = (bool)reader["U_IsDeleted"]
            };
        }

        public bool Register(AppUser u)
        {
            Connection connection = new Connection(_connectionString);
            string query = "UserRegister";
            Command cmd = new Command(query, true);

            cmd.AddParameter("email", u.Email);
            cmd.AddParameter("password", u.Password);
            cmd.AddParameter("name", u.Name);
            //cmd.AddParameter("isadmin", u.IsAdmin);
            try
            {
                return (connection.ExecuteNonQuery(cmd) > 0) ;
            }
            catch(SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
        public AppUser Login(string email, string pass)
        {
            Connection connection = new Connection(_connectionString);
            string query = "UserLogin";
            Command cmd = new Command(query, true);

            cmd.AddParameter("email", email);
            cmd.AddParameter("password", pass);
            try
            {
                return connection.ExecuteReader(cmd, Converter).First();
            }
            catch(SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
        
        public override IEnumerable<AppUser> GetAll()
        {
            Connection connection = new Connection(_connectionString);
            string query = "SELECT * FROM V_User";
            Command cmd = new Command(query, false);

            return connection.ExecuteReader(cmd, Converter);
        }
        public AppUser GetById(Guid Id)
        {
            Connection connection = new Connection(_connectionString);
            string Query = "SELECT * FROM V_User WHERE IdUser = @id";
            Command cmd = new Command(Query, false);
            cmd.AddParameter("Id", Id);

            return connection.ExecuteReader(cmd, Converter).FirstOrDefault();
        }

        public bool Update(AppUser u)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "UserUpdate";
            Command cmd = new Command(sql, true);

            cmd.AddParameter("email", u.Email);
            cmd.AddParameter("name", u.Name);
            cmd.AddParameter("password", u.Password);
            cmd.AddParameter("userid", u.IdUser);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool UpdatePassword(Guid userId, string actualPass, string newPass)
        {
            Connection connection = new Connection(_connectionString);
            string sql = "UserPasswordUpdate";
            Command cmd = new Command(sql, true);

            cmd.AddParameter("actualPassword", actualPass);
            cmd.AddParameter("newPassword", newPass);
            cmd.AddParameter("userid", userId);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
        public bool Undelete(Guid Id)
        {
            Connection connection = new Connection(_connectionString);
            string Query = "UPDATE AppUser SET U_IsDeleted = 0 WHERE IdUser = @id";
            Command cmd = new Command(Query, false);
            cmd.AddParameter("id", Id);

            return connection.ExecuteNonQuery(cmd) >= 0 ;
        }

        public bool Delete(Guid Id)
        {
            Connection connection = new Connection(_connectionString);
            string Query = "UPDATE AppUser SET U_IsDeleted = 1 WHERE IdUser = @id";
            Command cmd = new Command(Query, false);
            cmd.AddParameter("id", Id);

            return connection.ExecuteNonQuery(cmd) == 1;
        }

    }
}
