using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using SqlMigrator.Core.ExecutionBuilder;

namespace SqlMigrator.Core.MysqlMigrator
{
    public class MySqlMigrationDetector
    {
        public MigrationContext MigrationContext { get; private set; } = new MigrationContext();
        public MySqlMigrationDetector(SqlBlinkerConfig sqlBlinkerConfig)
        {
            this.MigrationContext.SqlBlinkerConfig = sqlBlinkerConfig;
            string srcStr =
               $"server={this.MigrationContext.SqlBlinkerConfig.Source.Host};user={this.MigrationContext.SqlBlinkerConfig.Source.UserName};password={this.MigrationContext.SqlBlinkerConfig.Source.Password}";
            this.MigrationContext.Source = new MySqlConnection(srcStr);
            this.MigrationContext.Source.Open();


            string targetStr =
                $"server={this.MigrationContext.SqlBlinkerConfig.Target.Host};user={this.MigrationContext.SqlBlinkerConfig.Target.UserName};password={this.MigrationContext.SqlBlinkerConfig.Target.Password}";
            this.MigrationContext.Target = new MySqlConnection(srcStr);
            this.MigrationContext.Target.Open();

        }

        public DescriptiveCommands Detect()
        {
            var cmds = new DescriptiveCommands();
            MySqlTableDiffDetector table = new MySqlTableDiffDetector(this.MigrationContext);
            cmds.CreateTables = table.detrectCreate();
            cmds.DropTables = table.detrectDrop();
            return cmds;

        }
    }

    public class MysqlMigrator{
     
        private List<string> allCommands = new List<string>();
        private readonly MigrationContext ctx;

           public MysqlMigrator(MigrationContext migrationContext){
            this.ctx = migrationContext;
        }

        public void Migrate(DescriptiveCommands commands){
            MysqlMigrationFacade facade = new MysqlMigrationFacade(this.ctx);
            foreach (var table in commands.DropTables.Tables)
            {
                this.ExecuteOrStack(facade.DropTable(table));
            }

            Console.WriteLine(this.BuildSingleScript());
            
        }

        private void ExecuteOrStack(SqlCommand sqlCommand)
        {
            allCommands.Add($"/** {sqlCommand.Comments} **/");
            allCommands.Add(sqlCommand.Command);
        }

        public string BuildSingleScript(){
            return string.Join(";\n",this.allCommands);
        }
    }

    public class MysqlMigrationFacade{
        private readonly MigrationContext ctx;

        public MysqlMigrationFacade(MigrationContext ctx)
        {
            this.ctx = ctx;
        }
        public SqlCommand DropTable(string table){
            return new SqlCommand{
                 Command =   $"DROP TABLE {table}",
                 Comments = $"The Table {table} should be removed"
                 };
        }
    }


}