using Contract.Attributes;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
    c.SchemaFilter<ConditionalPropertySchemaFilter>(); // Register the custom schema filter
});
builder.Services.AddTransient<GlobalHandlingException>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureRepository();
builder.Services.ConfigureService();
builder.Services.ConfigureMapster();
builder.Services.ConfigureJwtGenerator(builder.Configuration);


var app = builder.Build();
app.UseCors("CorsPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
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