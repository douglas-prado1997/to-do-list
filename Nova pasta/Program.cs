using Microsoft.EntityFrameworkCore;
using todo;
using todo.Models.Users;
using todo.Models.Tasks;
using todo.Util;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Adicionando vari�veis de ambiente
builder.Configuration.AddEnvironmentVariables();

// Adicionando Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new Exception("String de conex�o com o banco n�o encontrada");
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<PasswordService>();
builder.Services.AddSingleton<JwtTokenService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("LiberarAcessoFrontend", policy =>
    {
        policy.WithOrigins("*")  
              .AllowAnyMethod() 
              .AllowAnyHeader()  
              .SetPreflightMaxAge(TimeSpan.FromHours(1));  
    });
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();

app.UseForwardedHeaders();
app.UseSwagger();
app.UseSwaggerUI();
app.UseDeveloperExceptionPage();

app.UseCors("LiberarAcessoFrontend");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/health", async (UserContext userContext, TaskContext taskContext) =>
{
    try
    {
        await userContext.Database.CanConnectAsync();
        await taskContext.Database.CanConnectAsync();

        return Results.Ok("API est� funcionando! Conex�o com o banco de dados est� OK.");
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro na conex�o com o banco de dados: {ex.Message}");
    }
});


app.Run();