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
                                                    Database=EigenMaaltijd;
                                                    Uid=root;
                                                    Pwd=Onin2003!;");
            return _db;
        }


    }
}
