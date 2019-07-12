using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace domain.NHibernateHelper
{
    public class NHibernateHelper
    {
        public static ISessionFactory CreateSessionFactory()
        {
            {
                return Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012.ConnectionString("Server=.; Database=WebAPI; Trusted_Connection=True; MultipleActiveResultSets=true"))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Product>())
                    .ExposeConfiguration(cfg=> {cfg.SetInterceptor( new SqlStatementInterceptor());
                     new SchemaExport(cfg).Execute(true, false, false); })
                    .BuildSessionFactory();
                    //add from assembly of product
            }
        }
        public class SqlStatementInterceptor : EmptyInterceptor
        {
            public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
            {
                Console.WriteLine(sql.ToString());
                return sql;
            }
        }
    }
}