using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using stage1.Data;
using stage1.Endpoints;

var builder = WebApplication.CreateBuilder(args);


var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(conn));

builder.Services.AddValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "room-booking-api",
        Version = "V1",
        Description = "BackEnd para aplicação de reservas de salas"
    });
});


var app = builder.Build();
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapReservasEndpoints();

app.Run();
