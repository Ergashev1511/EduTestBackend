using EduTestApi.Configuration;
using EduTestApi.DependencyExtension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRepositories();
builder.ConfigurationValidators();
builder.Services.AddMediatRHandlers();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddDbContextes(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsImpelementationPolicy", builder => builder.WithOrigins("*")
                     .AllowAnyMethod()
                     .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseUrls("http://+:8080");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCorsImpelementationPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
