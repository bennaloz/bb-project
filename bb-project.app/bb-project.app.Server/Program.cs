// Support --migrate-only flag for CI/CD pre-deploy migration step.
// When this flag is passed, run migrations and exit without starting the web server.
if (args.Contains("--migrate-only"))
{
    var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
        ?? throw new InvalidOperationException("ConnectionStrings__DefaultConnection environment variable is not set.");
    bb_project.app.Server.MigrationRunner.RunMigrations(connectionString);
    return;
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Run database migrations at startup using DbUp.
// DbUp tracks applied scripts in the SchemaVersions table and only runs new ones.
// The connection string is read from the configuration (set as an Azure App Setting in production).
var migrationConnectionString = app.Configuration.GetConnectionString("DefaultConnection");
if (!string.IsNullOrEmpty(migrationConnectionString))
{
    bb_project.app.Server.MigrationRunner.RunMigrations(migrationConnectionString);
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
