using System;
using Evoflare.API.Configuration;
using Evoflare.API.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Evoflare.API.Core
{
    public static class LoggerFactoryExtensions
    {
        public static void UseSerilog(
            this IApplicationBuilder appBuilder,
            IHostingEnvironment env,
            IApplicationLifetime applicationLifetime,
            GlobalSettings globalSettings)
        {
            if (env.IsDevelopment())
            {
                return;
            }

            if (CoreHelpers.SettingHasValue(globalSettings?.Sentry.Dsn))
            {
                appBuilder.AddSentryContext();
            }
            applicationLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
        }

        public static ILoggingBuilder AddSerilog(
            this ILoggingBuilder builder,
            WebHostBuilderContext context,
            Func<LogEvent, bool> filter = null)
        {
            if (context.HostingEnvironment.IsDevelopment())
            {
                return builder;
            }
            var globalSettings = new GlobalSettings();
            ConfigurationBinder.Bind(context.Configuration.GetSection("GlobalSettings"), globalSettings);

            var config = new LoggerConfiguration()
                .Enrich.FromLogContext();

            if (CoreHelpers.SettingHasValue(globalSettings?.Sentry.Dsn))
            {
                config.WriteTo.Sentry(globalSettings.Sentry.Dsn)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Project", globalSettings.ProjectName)
                    .Destructure.With<HttpContextDestructingPolicy>()
                    .Filter.ByExcluding(e => e.Exception?.CheckIfCaptured() == true);
            }
            if (CoreHelpers.SettingHasValue(globalSettings.LogDirectory))
            {
                config.WriteTo.RollingFile($"{globalSettings.LogDirectory}/{globalSettings.ProjectName}/{{Date}}.txt")
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Project", globalSettings.ProjectName);
            }

            var serilog = config.CreateLogger();
            builder.AddSerilog(serilog);

            return builder;
        }
    }
}