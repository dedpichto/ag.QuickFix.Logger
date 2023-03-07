using ag.QuickFix.Logger.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuickFix;
using System.Linq;

namespace ag.QuickFix.Logger
{
    internal class QuickFixLogger : IQuickFixLogger
    {
        private readonly ILogger<QuickFixLogger> _logger;
        private readonly QuickFixLoggerSettings _settings;
        private readonly object _lock = new();

        public QuickFixLogger(ILogger<QuickFixLogger> logger,
            IOptions<QuickFixLoggerSettings> options)
        {
            _logger = logger;
            _settings = options.Value;
        }

        public void Clear() { }
        public void OnEvent(string s)
        {
            lock (_lock)
            {
                if (_logger == null || _logger is NullLog) return;
                if (!_settings.LogEvents) return;
                if (string.IsNullOrEmpty(_settings.PrefixEventMessage))
                    _logger.LogInformation(s);
                else
                    _logger.LogInformation($"{_settings.PrefixEventMessage} {s}");
            }
        }
        public void OnIncoming(string msg)
        {
            lock (_lock)
            {
                if (_logger == null || _logger is NullLog) return;
                if (!_settings.LogMessages) return;
                if (isMessageExcluded(msg)) return;
                if (string.IsNullOrEmpty(_settings.PrefixIncomingMessage))
                    _logger.LogInformation(msg);
                else
                    _logger.LogInformation($"{_settings.PrefixIncomingMessage} {msg}");
            }
        }
        public void OnOutgoing(string msg)
        {
            lock (_lock)
            {
                if (_logger == null || _logger is NullLog) return;
                if (!_settings.LogMessages) return;
                if (isMessageExcluded(msg)) return;
                if (string.IsNullOrEmpty(_settings.PrefixOutgoingMessage))
                    _logger.LogInformation(msg);
                else
                    _logger.LogInformation($"{_settings.PrefixOutgoingMessage} {msg}");
            }
        }
        public void Dispose() { }

        private bool isMessageExcluded(string msg)
        {
            var arr = msg.Split('\x01');
            if (_settings.ExcludedMsgTypes != null && _settings.ExcludedMsgTypes.Any())
            {
                foreach (var tag in _settings.ExcludedMsgTypes)
                {
                    if (arr.Any(a => a == $"35={tag}"))
                    {
                        return true; ;
                    }
                }
            }
            return false;
        }
    }
}
