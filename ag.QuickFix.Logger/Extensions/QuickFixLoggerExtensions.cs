using ag.QuickFix.Logger.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ag.QuickFix.Logger.Extensions
{
    /// <summary>
    /// Represents a <see cref="Logger"/> extentions.
    /// </summary>
    public static class QuickFixLoggerExtensions
    {
        /// <summary>
        /// Appends the registration of <see cref="QuickFixLoggerFactory"/> and <see cref="QuickFixLogger"/> services to <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns><see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddQuickFixLogger(this IServiceCollection services)
        {
            services.AddTransient<IQuickFixLogger, QuickFixLogger>();
            services.AddTransient<IQuickFixLoggerFactory, QuickFixLoggerFactory>();
            return services;
        }
        /// <summary>
        /// Appends the registration of <see cref="QuickFixLoggerFactory"/> and <see cref="QuickFixLogger"/> services to <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configurationSection">The <see cref="IConfigurationSection"/> being bound.</param>
        /// <returns><see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddQuickFixLogger(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            services.AddTransient<IQuickFixLogger, QuickFixLogger>();
            services.AddTransient<IQuickFixLoggerFactory, QuickFixLoggerFactory>();
            if (configurationSection == null)
                return services;
            services.Configure<QuickFixLoggerSettings>(opts =>
            {
                var children = configurationSection.GetChildren();
                if (children == null || !children.Any()) return;
                if (children.Any(c => c.Key == nameof(opts.LogMessages)))
                    opts.LogMessages = configurationSection.GetValue<bool>(nameof(opts.LogMessages));
                if (children.Any(c => c.Key == nameof(opts.LogEvents)))
                    opts.LogEvents = configurationSection.GetValue<bool>(nameof(opts.LogEvents));
                if (children.Any(c => c.Key == nameof(opts.PrefixIncomingMessage)))
                    opts.PrefixIncomingMessage = configurationSection.GetValue<string>(nameof(opts.PrefixIncomingMessage));
                if (children.Any(c => c.Key == nameof(opts.PrefixOutgoingMessage)))
                    opts.PrefixOutgoingMessage = configurationSection.GetValue<string>(nameof(opts.PrefixOutgoingMessage));
                if (children.Any(c => c.Key == nameof(opts.PrefixEventMessage)))
                    opts.PrefixEventMessage = configurationSection.GetValue<string>(nameof(opts.PrefixEventMessage));
                if (children.Any(c => c.Key == nameof(opts.ExcludedMsgTypes)))
                    opts.ExcludedMsgTypes = configurationSection.GetSection(nameof(opts.ExcludedMsgTypes)).Get<int[]>();
            });
            return services;
        }
        /// <summary>
        /// Appends the registration of <see cref="QuickFixLoggerFactory"/> and <see cref="QuickFixLogger"/> services to <see cref="IServiceCollection"/> and configures the options.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configureOptions">The action used to configure the options.</param>
        /// <returns><see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddQuickFixLogger(this IServiceCollection services, Action<QuickFixLoggerSettings> configureOptions)
        {
            services.AddTransient<IQuickFixLogger, QuickFixLogger>();
            services.AddTransient<IQuickFixLoggerFactory, QuickFixLoggerFactory>();
            if (configureOptions == null)
                return services;
            services.Configure(configureOptions);
            return services;
        }
    }
}
