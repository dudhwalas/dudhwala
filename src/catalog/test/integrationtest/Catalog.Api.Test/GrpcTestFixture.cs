﻿#region Copyright notice and license

// Copyright 2019 The gRPC Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Volo.Abp.Data;
using Volo.Abp.Guids;
using Xunit.Abstractions;

namespace Catalog.Api.Test
{
    public delegate void LogMessage(LogLevel logLevel, string categoryName, EventId eventId, string message, Exception? exception);

    public class GrpcTestFixture : IDisposable
    {
        private TestServer? _server;
        private HttpMessageHandler? _handler;
        private Action<IWebHostBuilder>? _configureWebHost;
        public IGuidGenerator? GuidGenerator { get; private set; }
        public event LogMessage? LoggedMessage;

        public GrpcTestFixture()
        {
            LoggerFactory = new LoggerFactory();
            LoggerFactory.AddProvider(new ForwardingLoggerProvider((logLevel, category, eventId, message, exception) =>
            {
                LoggedMessage?.Invoke(logLevel, category, eventId, message, exception);
            }));
        }

        public void ConfigureWebHost(Action<IWebHostBuilder> configure)
        {
            _configureWebHost = configure;
        }

        private async Task EnsureServer()
        {
            if (_server == null)
            {
                var builder = WebApplication.CreateBuilder();
                builder.Host.UseAutofac();
                builder.WebHost.UseTestServer();
                builder.Services.AddSingleton<ILoggerFactory>(LoggerFactory);
                builder.Services.AddSingleton<IDataSeedContributor, CatalogDataSeedContributor>();
                _configureWebHost?.Invoke(builder.WebHost);
                await builder.AddApplicationAsync<CatalogApiModule>();
                var app = builder.Build();
                await app.InitializeApplicationAsync();
                await app.StartAsync();
                _server = app.GetTestServer();
                _handler = _server.CreateHandler();
                GuidGenerator = app.Services.GetRequiredService<IGuidGenerator>();
                var dataSeeder = app.Services.GetRequiredService<IDataSeedContributor>();
                await dataSeeder.SeedAsync(new DataSeedContext());
            }
        }

        public LoggerFactory LoggerFactory { get; }

        public async Task<HttpMessageHandler> GetHandler()
        {
            await EnsureServer();
            return _handler!;
        }

        public void Dispose()
        {
            _handler?.Dispose();
            _server?.Dispose();
        }

        public IDisposable GetTestContext(ITestOutputHelper outputHelper)
        {
            return new GrpcTestContext(this, outputHelper);
        }
    }
}