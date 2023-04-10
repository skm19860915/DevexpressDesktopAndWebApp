using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using Microsoft.Extensions.Configuration;
//using Blitzer.Services;
using System.Data.SqlClient;
using NUnit.Framework;

/*
* https://blog.sanderaernouts.com/database-testing-with-nunit
*/
namespace NUnitTests.Database
{
    public class SqlDatabase : IDisposable
    {
        public SqlDatabase(TestContext context)
        {
            var randomPostfix = context.Random.GetString(6, "abcdefghijklmnopqrstuvw0123456789");
            var shortClassName = context.Test.ClassName.Substring(context.Test.ClassName.LastIndexOf(".", StringComparison.Ordinal) + 1);
            Name = $"{shortClassName}_{randomPostfix}";
            ConnectionString = $"server=.\\SQLEXPRESS;Initial Catalog={Name}; Integrated Security=True;multipleactiveresultsets=True;";
        }

        public string ConnectionString { get; }
        public string Name { get; }

        private void DropIfExists()
        {
            const string dropDatabaseSql =
            "if (select DB_ID('{0}')) is not null\r\n"
            + "begin\r\n"
            + "alter database [{0}] set offline with rollback immediate;\r\n"
            + "alter database [{0}] set online;\r\n"
            + "drop database [{0}];\r\n"
            + "end";

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    var sqlToExecute = string.Format(dropDatabaseSql, connection.Database);

                    var command = new SqlCommand(sqlToExecute, connection);

                    Console.WriteLine($"Attempting to drop database {connection.Database}");
                    command.ExecuteNonQuery();
                    Console.WriteLine("Database is dropped");
                }
            }
            catch (SqlException sqlException)
            {
                if (sqlException.Message.StartsWith("Cannot open database"))
                {
                    Console.WriteLine("Database did not exist.");
                    return;
                }

                throw;
            }
        }

        public void Dispose()
        {
            DropIfExists();
        }
    }
}
