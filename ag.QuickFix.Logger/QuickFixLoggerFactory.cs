using QuickFix;
using System;
using Microsoft.Extensions.DependencyInjection;
using ag.QuickFix.Logger.Settings;
using Microsoft.Extensions.Options;

namespace ag.QuickFix.Logger
{
    internal class QuickFixLoggerFactory : IQuickFixLoggerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly QuickFixLoggerSettings _settings;

        public QuickFixLoggerFactory(IServiceProvider serviceProvider, IOptions<QuickFixLoggerSettings> options)
        {
            _serviceProvider = serviceProvider;
            _settings = options.Value;
        }

        public ILog Create(SessionID sessionID)
        {
            if (!_settings.LogEvents && !_settings.LogMessages)
                return new NullLog();
            return _serviceProvider.GetService<IQuickFixLogger>();
        }
    }
}
