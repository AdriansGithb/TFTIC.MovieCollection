using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace MovieCollectionDAL.Services
{
    public abstract class BaseService<TResult>
    {
        private string _tableName;

        protected string _connectionString;

        public BaseService(string tableName, string connectionString)
        {
            _tableName = tableName;
            _connectionString = connectionString;
        }
        internal abstract TResult Converter(IDataRecord reader);

        public virtual TResult GetById(int Id)
        {
            Connection connection = new Connection(_connectionString);
            string Query = "SELECT * FROM " + _tableName + " WHERE Id = @id";
            Command cmd = new Command(Query, false);
            cmd.AddParameter("Id", Id);

            return connection.ExecuteReader(cmd, Converter).FirstOrDefault();
        }

        public virtual IEnumerable<TResult> GetAll()
        {
            Connection connection = new Connection(_connectionString);
            string Query = "SELECT * FROM [" + _tableName + "]";
            Command cmd = new Command(Query, false);

            return connection.ExecuteReader(cmd, Converter);
        }

        public virtual bool Delete(int Id)
        {
            Connection connection = new Connection(_connectionString);
            string Query = "DELETE FROM [" + _tableName + "] WHERE Id = @id";
            Command cmd = new Command(Query, false);
            cmd.AddParameter("id", Id);

            return connection.ExecuteNonQuery(cmd) == 1;
        }
    }
}
