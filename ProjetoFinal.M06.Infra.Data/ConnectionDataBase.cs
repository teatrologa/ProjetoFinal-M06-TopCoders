using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProjetoFinal.M06.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal.M06.Infra.Data
{
    public class ConnectionDataBase : IConnectionDataBase
    {
        private readonly IConfiguration _configuration;
        public ConnectionDataBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
