using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace DinnerTime.Api.Repositories
{
    public abstract class RepositoryBase
    {
        private string _connectionString;

        protected RepositoryBase()
        {
#if DEBUG
            const string PATH = "db.credential";
#else
            const string PATH = "/etc/dinnertime/db.credential";
#endif
            var password = File.ReadAllText(PATH);
			_connectionString = string.Format("Server=db.dinnertime.io;Database=DinnerTime;User Id=dinnertime;Password={0};", password);
        }

        protected async Task<IDbConnection> ConnectAsync()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync().ConfigureAwait(false);
            return connection;
        }
    }
}
