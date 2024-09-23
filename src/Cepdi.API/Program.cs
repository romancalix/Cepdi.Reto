using Cepdi.Persistence.Extentions;
using Cepdi.Application.UseCases;
using Cepdi.Application.UseCases.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInjectionPersistence();
builder.Services.AddInjectionApplication();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
    //builder.WithOrigins("http://localhost:5225")
    //       .AllowAnyMethod()
    //       .AllowAnyHeader();
    //builder.WithOrigins("http://localhost:7058")
    //       .AllowAnyMethod()
    //       .AllowAnyHeader();
}));

//builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();

var app = builder.Build();


app.UseCors("MyPolicy");

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
