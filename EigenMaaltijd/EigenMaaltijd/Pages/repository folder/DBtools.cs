using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EigenMaaltijd.Pages
{
    public class DBtools
    {
        public IDbConnection Connect()
        {
            IDbConnection _db = new MySqlConnection(@"Server=localhost;
                                                    Port=3306;
                                                    Database=periode3;
                                                    Uid=root;
                                                    Pwd=RyFYCz%8zKd9#");
            return _db;
        }


    }
}
