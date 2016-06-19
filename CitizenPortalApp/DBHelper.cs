using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPortalApp
{
    class DBHelper
    {
        public static string ConnectionString()
        {
            return @"Database=citizenPunjab;Data Source=ap-cdbr-azure-southeast-b.cloudapp.net;User Id=b0a7b9b1bc1fa7;Password=fa4e3b97; SslMode=none";
        }
    }
}
