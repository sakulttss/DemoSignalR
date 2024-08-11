using Flurl.Http;
using Microsoft.AspNetCore.SignalR.Client;

Console.Write("Please enter your name: ");
var name = Console.ReadLine();

try
{
    const string Host = "http://localhost:5215";
    var token = await $"{Host}/api/jwt/login?username={name}".GetStringAsync();
    var connection = new HubConnectionBuilder()
        .WithUrl($"{Host}/chat", cfg => cfg.AccessTokenProvider = () => Task.FromResult<string?>(token))
        .Build();

    connection.Closed += (error) => Console.Out.WriteLineAsync("Connection closed");

    const string Topic = "broadcastMessage";
    connection.On<string, string>(Topic, async (user, message) =>
    {
        if (user == name) return;
        await Console.Out.WriteLineAsync($"{user}: {message}");
    });

    await connection.StartAsync();
    await connection.SendAsync(Topic, "_SYSTEM_", $"{name} JOINED");
    Console.WriteLine("Connection started, Enter message (or 'exit' to close).");

    var message = string.Empty;
    do
    {
        message = Console.ReadLine();
        if (message != "exit")
        {
            await connection.SendAsync(Topic, name, message);
        }
    } while (message != "exit");
    await connection.DisposeAsync();
    Console.WriteLine("Connection closed");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}