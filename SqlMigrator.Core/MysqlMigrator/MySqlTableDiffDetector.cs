using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using SqlMigrator.Core.ExecutionBuilder;
using SqlMigrator.Core.MySqlUtils;

namespace SqlMigrator.Core.MysqlMigrator
{
    public partial class MySqlTableDiffDetector : ISqlTableDiifDetectorMigrator
    {
        private readonly MigrationContext ctx;

        public MySqlConnection SourceMysqlConnection { get; private set; }
        public MySqlConnection TargetMysqlConnection { get; private set; }


        public MySqlTableDiffDetector(MigrationContext ctx)
        {
            this.ctx = ctx;
        }



        public DropTables detrectDrop()
        {
            var select = new MysqlSelectAsObject<SqlTableDatailsObject>(this.ctx.Source);
            var sourceTables = select.Execute($"SELECT table_name FROM `information_schema`.`tables`  WHERE table_schema = '{this.ctx.SqlBlinkerConfig.Source.DatabaseName}'");
            var targetTables = select.Execute($"SELECT table_name FROM `information_schema`.`tables`  WHERE table_schema = '{this.ctx.SqlBlinkerConfig.Target.DatabaseName}'");
            var diffDrop = targetTables.Except(sourceTables);
            var dt = new DropTables();
            foreach (var item in diffDrop)
            {
                dt.Tables.Add(item.Name);
            }
            return dt;
        }

        public CreateTables detrectCreate()
        {
            var select = new MysqlSelectAsObject<SqlTableDatailsObject>(this.ctx.Source);
            var sourceTables = select.Execute($"SELECT table_name FROM `information_schema`.`tables`  WHERE table_schema = '{this.ctx.SqlBlinkerConfig.Source.DatabaseName}'");
            var targetTables = select.Execute($"SELECT table_name FROM `information_schema`.`tables`  WHERE table_schema = '{this.ctx.SqlBlinkerConfig.Target.DatabaseName}'");
           
            var diffCreate = sourceTables.Except(targetTables);
            var ct = new CreateTables();
            foreach (var item in diffCreate)
            {
                ct.Tables.Add(item.Name);
            }
            return ct;
        }
    }
}
