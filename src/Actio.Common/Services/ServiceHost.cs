using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Handlers;
using Actio.Common.RabbitMq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;
        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public void Run() => _webHost.Start();

        public static HostBuilder Create<TStrartup>(string [] args) where TStrartup : class
        {
            Console.Title = typeof(TStrartup).Name;
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables ()
                .AddCommandLine(args)
                .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<TStrartup>();

            return new HostBuilder(webHostBuilder.Build());
        }


        public abstract class BuilderBase
        {
            public abstract ServiceHost Build();
        }

        public class HostBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBusClient _bus;

            public HostBuilder(IWebHost webHost)
            {
                _webHost = webHost;
            }

            public BusBuilder UseRabbitMq ()
            {
                _bus = (IBusClient) _webHost.Services.GetService(typeof(IBusClient));
                return new BusBuilder(_webHost, _bus);
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }

        public class BusBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private readonly IBusClient _bus;
            public BusBuilder(IWebHost webHost, IBusClient bus)
            {
                _webHost = webHost;
                _bus = bus;
            }

            public BusBuilder SubscribeToCommand<TCommand> () where TCommand : ICommand
            {
                var serviceScopeFactory = _webHost.Services.GetService<IServiceScopeFactory>();
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var handler = (ICommandHandler<TCommand>)
                    //_webHost.Services.GetService(typeof(ICommandHandler<TCommand>));
                    scope.ServiceProvider.GetService(typeof(ICommandHandler<TCommand>));
                    
                    _bus.WithCommandHandlerAsync(handler);
                }
                return this;
            }

            public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
            {
                var serviceScopeFactory = _webHost.Services.GetService<IServiceScopeFactory>();
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var handler = (IEventHandler<TEvent>)
                    //_webHost.Services.GetService(typeof(IEventHandler<TEvent>));
                    scope.ServiceProvider.GetService(typeof(IEventHandler<TEvent>));
                    _bus.WithEventHandlerAsync(handler);
                }
                return this;
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }
    }
}
