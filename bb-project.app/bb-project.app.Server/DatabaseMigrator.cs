using DbUp;
using DbUp.Engine;

namespace bb_project.app.Server;

public static class DatabaseMigrator
{
    public static void RunMigrations(string connectionString)
    {
        EnsureDatabase.For.SqlDatabase(connectionString);

        UpgradeEngine upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(
                typeof(DatabaseMigrator).Assembly,
                s => s.StartsWith("bb_project.app.Server.Migrations."))
            .WithTransactionPerScript()
            .LogToConsole()
            .Build();

        DatabaseUpgradeResult result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            throw new Exception("Database migration failed.", result.Error);
        }
    }
}
