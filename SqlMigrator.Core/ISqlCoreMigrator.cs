namespace SqlMigrator.Core
{
    public interface ISqlCoreMigrator
    {
        void migrate(SqlBlinkerConfig sqlBlinkerConfig);
    }
}
