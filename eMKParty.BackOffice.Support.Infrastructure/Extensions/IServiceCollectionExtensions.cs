using System;
using eMKParty.BackOffice.Support.Application.Interfaces;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Common;
using eMKParty.BackOffice.Support.Domain.Common.Interfaces;
using eMKParty.BackOffice.Support.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace eMKParty.BackOffice.Support.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IMediator, Mediator>()
                .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>()
                .AddTransient<IDateTimeService, DateTimeService>()
                .AddTransient(typeof(IAesOperation), typeof(AesOperation))
                //.AddTransient<ITokenService, TokenService>()
                .AddTransient<IEmailService, EmailService>();
        }
    }
}

