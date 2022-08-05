using System.Collections.Generic;

namespace DCMS.Application.Configurations
{
    public class DataSettings
    {
        public bool ApplyDbMigrationsOnStartup { get; set; }
        public List<DbConnection> DbConnections { get; set; }
    }

    public class DbConnection
    {
        public string ConnectionString { get; set; }

        public string Source { get; set; }
    }
}
