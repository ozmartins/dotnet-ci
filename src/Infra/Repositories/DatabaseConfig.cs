namespace Infra.Repositories
{
    public class DatabaseConfig
    {
        public DatabaseConfig()
        {
            ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? string.Empty;
            DatabaseAlias = Environment.GetEnvironmentVariable("DATABASE_ALIAS") ?? string.Empty; ;
        }
        public string ConnectionString { get; }
        public string DatabaseAlias { get; }
    }
}
