using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.NTSYHospital.service
{
    class DBContextFactory
    {
        public static SqlSugarClient GetContext()
        {
            return DbContextServer.Context.Db;
        }
    }
}
