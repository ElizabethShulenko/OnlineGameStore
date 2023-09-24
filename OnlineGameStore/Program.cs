using OnlineGameStore.Core.Services;
using OnlineGameStore.DB;
using OnlineGameStore.DB.Entities;
using OnlineGameStore.DB.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddScoped(provider => new OnlineGameStoreDbContext(configuration.GetConnectionString("OnlineGameStoreDb") ?? String.Empty));

builder.Services.AddScoped<IRepository<Game>, Repository<Game>>();

builder.Services.AddScoped<IGameService, GameService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
