namespace SqlMigrator.Core.MySqlUtils
{

    public abstract class SqlObject{

    }

    [System.AttributeUsage(System.AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    sealed class SqlMemberAttribute : System.Attribute
    {
        readonly string map;

        public SqlMemberAttribute(string map)
        {
            this.map = map;
        }

        public string Map
        {
            get { return map; }
        }

    }
}