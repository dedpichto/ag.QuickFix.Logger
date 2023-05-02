using ag.QuickFix.Logger.Events;
using ag.QuickFix.Logger.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuickFix;

namespace ag.QuickFix.Logger
{
    internal class QuickFixLogger : IQuickFixLogger
    {
        private const char _QF_DELIMITER = '\x01';
        private readonly ILogger<QuickFixLogger> _logger;
        private readonly IQuickFixEventsRaiser _eventsRaiser;
        private readonly QuickFixLoggerSettings _settings;
        private readonly object _lock = new();

        public QuickFixLogger(ILogger<QuickFixLogger> logger,
            IQuickFixEventsRaiser eventsRaiser,
            IOptions<QuickFixLoggerSettings> options)
        {
            _logger = logger;
            _eventsRaiser = eventsRaiser;
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
            if (_settings.AllowRaisingForEvents)
            {
                if (_settings.AllowAsyncEvents)
                    _ = _eventsRaiser.OnEventAsync(this, new QuickFixEventArgs(s));
                else
                    _eventsRaiser.OnEvent(this, new QuickFixEventArgs(s));
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
            if (_settings.AllowRaisingForIncoming && !isMessageExcluded(msg))
            {
                if (_settings.AllowAsyncEvents)
                    _ = _eventsRaiser.OnIncomingAsync(this, new QuickFixEventArgs(msg));
                else
                    _eventsRaiser.OnIncoming(this, new QuickFixEventArgs(msg));
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
            if (_settings.AllowRaisingForOutgoing && !isMessageExcluded(msg))
            {
                if (_settings.AllowAsyncEvents)
                    _ = _eventsRaiser.OnOutgoingAsync(this, new QuickFixEventArgs(msg));
                else
                    _eventsRaiser.OnOutgoing(this, new QuickFixEventArgs(msg));
            }
        }

        public void Dispose() { }

        private bool isMessageExcluded(string msg)
        {
            if (_settings.ExcludedMsgTypes != null)
            {
                foreach (var tag in _settings.ExcludedMsgTypes)
                {
                    var pattern = $"{_QF_DELIMITER}35={tag}{_QF_DELIMITER}";
                    if (msg.Contains(pattern))
                        return true;
                }
            }
            return false;
        }
    }
}
