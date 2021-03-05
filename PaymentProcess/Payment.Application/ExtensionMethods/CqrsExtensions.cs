﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Payment.Application.ExtensionMethods
{
    public static class CQRSExtensions
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }

    }
}
