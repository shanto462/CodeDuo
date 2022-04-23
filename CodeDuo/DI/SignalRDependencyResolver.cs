﻿using Microsoft.AspNet.SignalR;

namespace CodeDuo.DI
{
    public class SignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public SignalRDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object GetService(Type serviceType)
        {
            var service = _serviceProvider.GetService(serviceType);

            return service ?? base.GetService(serviceType);
        }

    }
}
