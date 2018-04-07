namespace SqlMigrator.Core
{
    public class SqlBlinkerConfig
    {
        /**
         * Indicates weather to wait for a response after preparing the command
         */
        public bool Interactive { get; set; }

        /**
         * Indicates whether to detect renames in columns and tables.
         * If false, you will be pushed to DROP the column/table
         */
        public bool DetectRenames { get; set; }


        public WorkingMode WorkingMode { get; set; }

        public SqlConnectionDetails Source { get; set; }
        public SqlConnectionDetails Target { get; set; }
    }


}
