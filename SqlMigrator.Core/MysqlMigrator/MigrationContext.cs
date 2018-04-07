using MySql.Data.MySqlClient;

namespace SqlMigrator.Core.MysqlMigrator
{


    
        public class MigrationContext
        {
            public SqlBlinkerConfig SqlBlinkerConfig { get; set; }
            public MySqlConnection Source { get; set; }
            public MySqlConnection Target { get; set; }
        }
    
}
