using Blog.Infrastructure.Database;
using Blog.Infrastructure.Factories;
using Blog.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("BlogStoreDatabase"));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SQLite
//builder.Services.AddEntityFrameworkSqlite().AddDbContext<MyDbContext>();
//builder.Services.AddScoped<IPostService, PostService>();
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IPostRepository, PostRepository>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();

// MongoDB
builder.Services.AddScoped<MongoDbFactory>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();