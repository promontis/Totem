using Acme.ProductImport;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Totem.Runtime.Hosting;
using Totem.Timeline.Hosting;
using Totem.Timeline.StreamsDb.Hosting;

namespace Totem.Sample.Service
{
  class Program
  {
    static async Task Main(string[] args)
    {
      var builder = new HostBuilder()
      .ConfigureHostConfiguration(configHost =>
      {
      })
      .ConfigureAppConfiguration((hostingContext, config) =>
      {
      })
      .ConfigureLogging((hostingContext, logging) =>
      {
      })
      .ConfigureServices((hostContext, services) =>
      {
        services.AddTotemRuntime();

        services.AddTimeline<ProductImportArea>(timeline =>
        {
          timeline.AddStreamsDb("", "sample");
        });
      });

      await builder.RunConsoleAsync();
    }
  }
}