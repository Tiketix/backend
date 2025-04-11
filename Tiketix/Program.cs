using AspNetCoreRateLimit;
using dotenv.net;
using Microsoft.OpenApi.Models;
using tiketix.Extensions;


DotEnv.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.ConfigureRepositoryManager();

builder.Services.ConfigureServiceManager();

builder.Services.ConfigureSqlContext();

builder.Services.AddAuthentication();

builder.Services.ConfigureIdentity();

builder.Services.ConfigureCors();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.AddMemoryCache();

builder.Services.ConfigureRateLimitingOptions();

builder.Services.AddHttpContextAccessor();

DotEnv.Load();

builder.Services.AddControllers().AddApplicationPart(typeof(EventClients.Presentation.AssemblyReference).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Tiketix API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseIpRateLimiting();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();







app.Run();


