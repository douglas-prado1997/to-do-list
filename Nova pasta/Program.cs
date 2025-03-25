using Microsoft.EntityFrameworkCore;
using todo;
using todo.Models.Users;
using todo.Models.Tasks;
using todo.Util;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Adicionando variáveis de ambiente
builder.Configuration.AddEnvironmentVariables();

// Adicionando Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new Exception("String de conexão com o banco não encontrada");
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

        return Results.Ok("API está funcionando! Conexão com o banco de dados está OK.");
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro na conexão com o banco de dados: {ex.Message}");
    }
});


app.Run();