using Final_Lab.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// setup cors
builder.Services.AddCors(C =>
{
    C.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader());
});

// DbContext
var serverVersion = new MySqlServerVersion(new Version(8,0,31));

builder.Services.AddDbContext<DataDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("Default"), serverVersion);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//cors
app.UseCors(
    options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader()
    );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
