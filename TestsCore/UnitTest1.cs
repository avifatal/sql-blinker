using System;
using Xunit;
using SqlMigrator.Core.MysqlMigrator;
using SqlMigrator.Core;
using MySql.Data.MySqlClient;
using SqlMigrator.Core.MySqlUtils;
using Newtonsoft.Json;

namespace TestsCore
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            var conf = new SqlBlinkerConfig();
            conf.Source = new SqlConnectionDetails
            {
                Host = "localhost",
                UserName = "root",
                Password = "",
                DatabaseName = "s"

            };

            conf.Target = new SqlConnectionDetails
            {
                Host = "localhost",
                UserName = "root",
                Password = "",
                DatabaseName = "t"
            };

            MySqlMigrationDetector migrator = new MySqlMigrationDetector(conf);
            //MysqlSelectAsObject<SqlTableDetailsObject> select = new MysqlSelectAsObject<SqlTableDetailsObject>(migrator.SourceMysqlConnection);
            //var res = select.Execute("select * from information_schema.TABLES");

            new MySqlCommand("drop database if exists `s`",migrator.MigrationContext.Target).ExecuteNonQuery();
            new MySqlCommand("drop database if exists `t`",migrator.MigrationContext.Target).ExecuteNonQuery();


            new MySqlCommand("create database s",migrator.MigrationContext.Target).ExecuteNonQuery();
            new MySqlCommand("create database t",migrator.MigrationContext.Target).ExecuteNonQuery();

            new MySqlCommand(@"CREATE TABLE `t`.`to_remove` (
                                `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
                                PRIMARY KEY (`id`)
                                ) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=utf8",migrator.MigrationContext.Target).ExecuteNonQuery();

            new MySqlCommand(@"CREATE TABLE `s`.`to_create` (
                                `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
                                PRIMARY KEY (`id`)
                                ) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=utf8",migrator.MigrationContext.Target).ExecuteNonQuery();

            var allCommands = migrator.Detect();

            MysqlMigrator mig = new MysqlMigrator(migrator.MigrationContext);
            mig.Migrate(allCommands);



            

        }
    }
}