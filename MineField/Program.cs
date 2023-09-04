// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MineField.Core;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<MineField.GameConsole>()
        .AddScoped<IChessBoard, ChessBoard>()
        .AddScoped<IMineBuilder, MineBuilder>();
    }).Build();


// Start the game.
host.Services.GetRequiredService<MineField.GameConsole>().RunGame();


Console.WriteLine("Good Bye!");
