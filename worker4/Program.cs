using Serilog;
using worker4;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "worker33";
    })
    .ConfigureLogging(logging =>
    {
        logging.AddSerilog();
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<Worker>();
        services.AddHostedService<Worker>();
    })
    .Build();
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(""+ Path.GetPathRoot(Environment.SystemDirectory)+ "\\ProgramData\\VeriketApp\\VeriketAppTest.txt")
    .CreateLogger();
try
{
    Log.Information("worker service is started");
    await host.RunAsync();
    return;

}
catch (Exception ex)
{
    Log.Fatal(ex, "worker service have error");

}
finally
{
    Log.CloseAndFlush();
}