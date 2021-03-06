﻿using System.Globalization;
using Autofac;
using AutofacSerilogIntegration;
using ImageConverterLib.Configuration;
using Serilog;
using Serilog.Events;

namespace ImageConverterLib.Library.AutofacModules
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var logLevel = LogEventLevel.Information;
            if (!ApplicationBuildConfig.DebugMode)
            {
                logLevel = LogEventLevel.Warning;
            }

            Log.Logger = new LoggerConfiguration()
                         .WriteTo.Console(LogEventLevel.Debug, standardErrorFromLevel: LogEventLevel.Error, formatProvider: CultureInfo.InvariantCulture)
                         .WriteTo.File(ApplicationBuildConfig.ApplicationLogFilePath(),
                             fileSizeLimitBytes: 1048576,
                             retainedFileCountLimit: 20,
                             restrictedToMinimumLevel: logLevel,
                             buffered: false,
                             outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.ff} [{Level}] {Message}{NewLine}{Exception}{Data}")
                         .Enrich.FromLogContext()
                         .MinimumLevel.Is(logLevel)
                         .CreateLogger();

            builder.RegisterLogger();
        }
    }
}