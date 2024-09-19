using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepdi.Persistence.Context
{
    public class ApplicationDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ApplicationDbContext(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connectionString = configuration.GetConnectionString("Default")!;
        }

        public IDbConnection CreateConnection => new SqlConnection(this._connectionString);
    }
}
