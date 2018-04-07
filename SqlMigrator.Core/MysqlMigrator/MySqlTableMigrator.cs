using System;
using SqlMigrator.Core.MySqlUtils;

namespace SqlMigrator.Core.MysqlMigrator
{

    public class SqlTableDatailsObject : SqlObject
    {
        [SqlMemberAttribute("TABLE_NAME")]
        public string Name { get; set; }
    }
}
