using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal.M06.Core.Interface
{
    public interface IConnectionDataBase
    {
        IDbConnection CreateConnection();
    }
}
