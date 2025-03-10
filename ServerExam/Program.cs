using Microsoft.EntityFrameworkCore;
using ServerExam.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("DataSource=Shop.db;Cache=Shared"));

var app = builder.Build();



app.UseHttpsRedirection();


app.Run();

