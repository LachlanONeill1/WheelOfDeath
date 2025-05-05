using LibEntity.NetFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibWebWheelOfDeath
{
    public static class Model
    {
        public static void Initialize(string connectionString = null)
        {
            Global.ConnectionString = connectionString ?? @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dbWheelofDeath;";
        }
    }
}
