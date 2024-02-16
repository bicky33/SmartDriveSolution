using Microsoft.Extensions.FileProviders;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureCors();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.AddTransient<GlobalHandlingException>();
builder.Services.ConfigureRepositorySOManager();
builder.Services.ConfigureServiceSOManager();
builder.Services.ConfigureServiceRequestSOManager();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalHandlingException>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.UseStaticFiles(
    new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
        RequestPath = new PathString("/Resources")
    }
);

app.MapControllers();
app.Run();
