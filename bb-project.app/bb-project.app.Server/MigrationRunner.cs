using DbUp;
using DbUp.Engine;
using System.Reflection;

namespace bb_project.app.Server;

/// <summary>
/// Runs pending SQL migration scripts from the bb-project.Database folder against Azure SQL.
/// Uses DbUp to track which scripts have already been applied (stored in a SchemaVersions table).
/// Called at application startup (and optionally as a pre-deploy step with --migrate-only).
/// </summary>
public static class MigrationRunner
{
    /// <summary>
    /// Executes any pending SQL scripts found in the Migrations/Scripts folder.
    /// Scripts are run in alphabetical order. Already-applied scripts are skipped.
    /// </summary>
    /// <param name="connectionString">Azure SQL connection string.</param>
    /// <exception cref="Exception">Thrown when one or more migration scripts fail.</exception>
    public static void RunMigrations(string connectionString)
    {
        var scriptsPath = GetScriptsPath();

        if (!Directory.Exists(scriptsPath))
        {
            Console.WriteLine($"[DbUp] Migrations folder not found at: {scriptsPath}. Skipping migrations.");
            return;
        }

        var upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsFromFileSystem(scriptsPath)
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.Error.WriteLine($"[DbUp] Migration failed: {result.Error?.Message}");
            throw new Exception($"Database migration failed: {result.Error?.Message}", result.Error);
        }

        Console.WriteLine("[DbUp] All migrations applied successfully.");
    }

    /// <summary>
    /// Returns the path to the SQL scripts folder.
    /// Looks for a "Migrations/Scripts" folder relative to the assembly location,
    /// which is populated during publish by the workflow copying scripts from bb-project.Database/.
    /// </summary>
    private static string GetScriptsPath()
    {
        // When running from the published output, scripts are copied to Migrations/Scripts/
        var assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? ".";
        return Path.Combine(assemblyDir, "Migrations", "Scripts");
    }
}
