using LogFinder.BusinesLogic.Models;
using LogFinder.BusinesLogic.Services;
using LogFinder.Presentation;
using LogFinder.Presentation.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
.ConfigureServices((context, services) =>
{
    services.ConfigureSettings<DirectorySettings>(context.Configuration, DirectorySettings.Section);
    services.AddSingleton<IDataParserService, DataParserService>();
    services.AddSingleton<IQueryExecutorService, QueryExecutorService>();
    services.AddSingleton<App>();
}).Build();

host.Services.GetRequiredService<App>().Run();
