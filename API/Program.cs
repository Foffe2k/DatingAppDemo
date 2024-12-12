using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Services används bla som dependencies för injections. Ramverket skapar dem när det startar 
// upp och håller själv koll på de objekten under runtime
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt => 
    {
        opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); //Hur man läser värde från config fil (i detta fall connection string ("DefaultConnection"))
    });

builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
// Ordningen har betydelse här
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));
app.MapControllers();

app.Run();
