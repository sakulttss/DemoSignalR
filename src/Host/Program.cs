using Host.Controllers;
using Host.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();//.AddAzureSignalR();
builder.Services.AddControllers();
builder.Services.AddCors(setup =>
{
    setup.AddDefaultPolicy(cfg =>
    {
        cfg
            .WithOrigins("http://localhost:5196")
            .AllowAnyHeader()
            .WithMethods("GET", "POST")
            .AllowCredentials();
    });
});
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = JwtController.Issuer,
            ValidAudience = JwtController.Audience,
            IssuerSigningKey = JwtController.SigningCreds.Key,
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Path.StartsWithSegments("/chat")
                    && string.IsNullOrWhiteSpace(context.Token)
                    && context.Request.Query.TryGetValue("access_token", out var token)
                    && !string.IsNullOrWhiteSpace(token))
                {
                    context.Token = token;
                }

                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chat");

app.Run();
