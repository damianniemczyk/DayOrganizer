using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess
{
    public sealed class DatabaseCore
    {
        public static DatabaseCore Instance { get; } = new DatabaseCore();


        public static void TestRun()
        {
            OpenSession();
            CloseSession();
        }

        public static ISession OpenSession()
        {
            return SessionFactoryHelper.SessionFactory.OpenSession();
        }

        public static void CloseSession()
        {
            SessionFactoryHelper.SessionFactory.Close();
        }
    }
}
