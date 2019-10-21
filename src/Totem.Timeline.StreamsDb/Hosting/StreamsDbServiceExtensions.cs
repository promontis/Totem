using System;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Totem.Runtime.Hosting;
using Totem.Runtime.Json;
using Totem.Timeline.Area;
using Totem.Timeline.Client;
using Totem.Timeline.Hosting;
using Totem.Timeline.Runtime;
using Totem.Timeline.StreamsDb.Client;
using Totem.Timeline.StreamsDb.Integration;
using Totem.Timeline.StreamsDb.Projection;

namespace Totem.Timeline.StreamsDb.Hosting
{
  /// <summary>
  /// Extends <see cref="ITimelineBuilder"/> to declare the EventStore timeline database
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public static class StreamsDbServiceExtensions
  {
    public static IStreamsDbTimelineBuilder AddStreamsDb(this ITimelineBuilder timeline, string connectionString, string areaName)
    {
      timeline.ConfigureServices(services =>
      {
        services.AddSingleton<ITimelineDb>(p => new TimelineDb(
          p.GetRequiredService<StreamsDbContext>(),
          p.GetRequiredService<IResumeProjection>(),
          p.GetRequiredService<IIntegrationSubscriber>()));

        services.AddSingleton(p => new StreamsDbContext(
          connectionString,
          areaName,
          p.GetRequiredService<IJsonFormat>(),
          p.GetRequiredService<AreaMap>()));

        services.AddSingleton<IResumeProjection, ResumeProjection>();
        services.AddSingleton<IIntegrationSubscriber, IntegrationSubscriber>();
        services.AddSingleton<IClientDb, ClientDb>();
      });

      return new EventStoreTimelineBuilder(timeline);
    }

    public static IStreamsDbTimelineBuilder BindOptionsToConfiguration(this IStreamsDbTimelineBuilder timeline, string key = "totem.timeline.eventStore") =>
      timeline.ConfigureServices(services => services.BindOptionsToConfiguration<StreamsDbTimelineOptions>(key));

    class EventStoreTimelineBuilder : IStreamsDbTimelineBuilder
    {
      readonly ITimelineBuilder _timeline;

      internal EventStoreTimelineBuilder(ITimelineBuilder timeline)
      {
        _timeline = timeline;
      }

      public IStreamsDbTimelineBuilder ConfigureServices(Action<IServiceCollection> configure)
      {
        _timeline.ConfigureServices(configure);

        return this;
      }
    }
  }
}