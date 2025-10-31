using TaskManager.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on port 5000
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000);
});

builder.Services.AddSingleton<TaskService>();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", p =>
        p.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());
});
var app = builder.Build();
app.UseCors("AllowFrontend");
app.MapControllers();
app.Run();
