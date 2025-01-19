using Microsoft.EntityFrameworkCore;
using hecotoBackend.Data; 

var builder = WebApplication.CreateBuilder(args);

// se sta usando sql posgrest

builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.Run();