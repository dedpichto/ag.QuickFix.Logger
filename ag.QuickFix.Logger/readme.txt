
// add section to settings file (optional)
{
  "QuickFixLoggerSettings": {
    "PrefixIncomingMessage": "FixIncoming",
    "PrefixOutgoingMessage": "FixOutgoing",
    "PrefixEventMessage": "FixEvent",
    "ExcludedMsgTypes": [
      "0"
    ]
  }
}

***************************************************************************************************

// add appropriate usings
using ag.QuickFix.Logger.Extensions;
using ag.QuickFix.Logger.Settings;

***************************************************************************************************

// register services with one of overloaded extension methods

// uses default settings
services.AddQuickFixLogger();
// uses configuration file settings
services.AddQuickFixLogger(configuration.GetSection(nameof(QuickFixLoggerSettings)));
// explicily sets settings
services.AddQuickFixLogger(opts =>
{
    opts.LogEvents = false;
    opts.ExcludedMsgTypes = new[] { 0 };
});

***************************************************************************************************

// inject IQuickFixLoggerFactory into your classes

private readonly IQuickFixLoggerFactory _quickFixLoggerFactory;

public MyClass(IQuickFixLoggerFactory quickFixLoggerFactory)
{
    _quickFixLoggerFactory = quickFixLoggerFactory;
}

// inject IQuickFixEventsRaiser if you are going to use events

private readonly IQuickFixLoggerFactory _quickFixLoggerFactory;
private readonly IQuickFixEventsRaiser _eventsRaiser;

public MyClass(IQuickFixLoggerFactory quickFixLoggerFactory, IQuickFixEventsRaiser eventsRaiser)
{
    _quickFixLoggerFactory = quickFixLoggerFactory;
    _eventsRaiser = eventsRaiser;
}
***************************************************************************************************

// substitute ILogFactory with IQuickFixLoggerFactory
var settings = new QuickFix.SessionSettings(file);
var storeFactory = new QuickFix.FileStoreFactory(settings);
var initiator = new QuickFix.Transport.SocketInitiator(_tradeClientApp, storeFactory, settings, _quickFixLoggerFactory);

// subscribe to evnts, if you chose to use any
_eventsRaiser.OnQuickFixEvent += (sender, e) => Console.WriteLine($"Event==>{e.MessageString}");
_eventsRaiser.OnQuickFixIncoming += (sender, e) => Console.WriteLine($"Incoming==>{e.MessageString}");
_eventsRaiser.OnQuickFixOutgoing += (sender, e) => Console.WriteLine($"Outgoing==>{e.MessageString}");