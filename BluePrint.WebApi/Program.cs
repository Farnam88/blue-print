using BluePrint.WebApi.Helpers;
using BluePrint.WebApi.Modules;

var builder = WebApplication.CreateBuilder(args);
builder.Environment.BuildConfigurationRoot();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BluePrint WebApi v1"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.Services.InitDataBase();

await app.RunAsync();

public partial class Program { }