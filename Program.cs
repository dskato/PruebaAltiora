var builder = WebApplication.CreateBuilder(args);

#region SERVICE INJECTION
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
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
var baseController = new APIControllerBase();

app.MapPost("client/addClient", async (IClientService clientService, ClientDto clientDto) =>
{

    var response = await clientService.AddClient(clientDto);
    return baseController.Response(response, ResponseCode.OK);

}).WithTags("User");

#endregion

app.Run();
