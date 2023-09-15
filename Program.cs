var builder = WebApplication.CreateBuilder(args);

#region SERVICE INJECTION
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProductService, ProductService>();
#endregion

#region CONFIGURATION
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
                            .Build();
var connectionString = configuration.GetConnectionString("MySqlConnection");
ServerExtension.ConfigureSQLServices(builder, connectionString);

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
var app = builder.Build();
app.UseSwagger();
app.UseCors("DevelopmentPolicy");
#endregion

#region API
app.MapGet("/", () => "Hello World!");
#endregion

app.Run();
