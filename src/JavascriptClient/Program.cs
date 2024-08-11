var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseRouting();
app.UseStaticFiles();

app.MapGet("/", () => "Javascript Client");

app.Run();
