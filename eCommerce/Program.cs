using eCommerce.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<UserService, UserServiceImpl>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
