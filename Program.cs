/*
Coded by: David Araujo
Date: 15/09/2023

*/

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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseCors("DevelopmentPolicy");
#endregion

#region API
var baseController = new APIControllerBase();


#region CLIENT CONTROLLERS
app.MapPost("client/AddClient", async (IClientService clientService, ClientDto clientDto) =>
{

    var response = await clientService.AddClient(clientDto);
    return baseController.Response(response, ResponseCode.OK);

}).WithTags("Client");

app.MapGet("client/GetAllClients", async (IClientService clientService) =>
{

    return baseController.Response(await clientService.GetAllClients());

}).WithTags("Client");

app.MapGet("client/GetClientById/{id}", async (IClientService clientService, int id) =>
{

    return baseController.Response(await clientService.GetClientById(id));

}).WithTags("Client");

app.MapPost("client/UpdateClient", async (IClientService clientService, ClientDto clientDto) =>
{

    var response = await clientService.UpdateClient(clientDto);
    return baseController.Response(response, ResponseCode.OK);

}).WithTags("Client");

app.MapDelete("client/DeleteClientById/{id}", async (IClientService clientService, int id) =>
{

    var response = await clientService.DeleteClientById(id);
    return baseController.Response(response, ResponseCode.OK);

}).WithTags("Client");
#endregion

#region  PRODUCT CONTROLLERS
app.MapPost("product/AddProduct", async (IProductService productService, ProductDto productDto) =>
{

    var response = await productService.AddProduct(productDto);
    return baseController.Response(response, ResponseCode.OK);

}).WithTags("Product");

app.MapGet("product/GetAllProduct", async (IProductService productService) =>
{

    return baseController.Response(await productService.GetAllProduct());

}).WithTags("Product");

app.MapGet("product/GetProducttById/{id}", async (IProductService productService, int id) =>
{

    return baseController.Response(await productService.GetProducttById(id));

}).WithTags("Product");

app.MapPost("product/UpdateProduct", async (IProductService productService, ProductDto productDto) =>
{

    var response = await productService.UpdateProduct(productDto);
    return baseController.Response(response, ResponseCode.OK);

}).WithTags("Product");

app.MapDelete("product/DeleteProductById/{id}", async (IProductService productService, int id) =>
{

    return baseController.Response(await productService.DeleteProductById(id));

}).WithTags("Product");
#endregion

#region ORDER CONTROLLERS
app.MapPost("order/AddOrder", async (IOrderService orderService, OrderDto orderDto) =>
{

    var response = await orderService.AddOrder(orderDto);
    return baseController.Response(response, ResponseCode.OK);

}).WithTags("Order");
app.MapGet("order/GetAllOrders", async (IOrderService orderService) =>
{

    return baseController.Response(await orderService.GetAllOrders());

}).WithTags("Order");
app.MapGet("order/GetOrderById/{id}", async (IOrderService orderService, string id) =>
{

    return baseController.Response(await orderService.GetOrderById(id));

}).WithTags("Order");
app.MapGet("order/GetOrderByClientId/{id}", async (IOrderService orderService, int id) =>
{

    return baseController.Response(await orderService.GetOrderByClientId(id));

}).WithTags("Order");
app.MapGet("order/GetOrderByProductId/{id}", async (IOrderService orderService, int id) =>
{

    return baseController.Response(await orderService.GetOrderByProductId(id));

}).WithTags("Order");
app.MapDelete("order/DeleteOrderById/{id}", async (IOrderService orderService, string id) =>
{

    return baseController.Response(await orderService.DeleteOrderById(id));

}).WithTags("Order");
#endregion
#endregion

app.MigrateDatabase();
app.MapSwagger();
app.UseSwaggerUI();
app.Run();
