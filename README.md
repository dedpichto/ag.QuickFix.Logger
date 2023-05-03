# ag.QuickFix.Logger

![Nuget](https://img.shields.io/nuget/v/ag.QuickFix.Logger)

QuickFix/n is the most potent and successful library for working with FIX protocol in a .NET environment.<br/>
However, logging all messages and events may be tricky, because, by default, QuickFix logs all of them to the console.<br/>
The idea is to substitute ILogFactory with your own one.<br>
The only requirement is to use some logging provider (such as SeriLog or any other of your choice).

## Usage

1. Add section to settings file (optional)

```csharp
{
  "QuickFixLoggerSettings": {
    "PrefixIncomingMessage": "FixIncoming",
    "PrefixOutgoingMessage": "FixOutgoing",
    "PrefixEvent": "FixEvent",
    "ExcludedMsgTypes": [
      "0"
    ]
  }
}
```

2. Add appropriate usings

```csharp
using ag.QuickFix.Logger.Extensions;
using ag.QuickFix.Logger.Settings;
```

3. Register services with one of overloaded extension methods

```csharp
// uses default settings
services.AddQuickFixLogger();
// uses configuration file settings
services.AddQuickFixLogger(configuration.GetSection(nameof(QuickFixLoggerSettings)));
// explicily sets settings
services.AddQuickFixLogger(opts =>
{
    opts.LogEvents = false;
    opts.ExcludedMsgTypes = new[] { "0" };
});
```

4. Inject IQuickFixLoggerFactory into your classes

```csharp
private readonly IQuickFixLoggerFactory _quickFixLoggerFactory;

public MyClass(IQuickFixLoggerFactory quickFixLoggerFactory)
{
    _quickFixLoggerFactory = quickFixLoggerFactory;
}
```

5. Inject IQuickFixEventsRaiser if you are going to use events

```csharp
private readonly IQuickFixLoggerFactory _quickFixLoggerFactory;
private readonly IQuickFixEventsRaiser _eventsRaiser;

public MyClass(IQuickFixLoggerFactory quickFixLoggerFactory, IQuickFixEventsRaiser eventsRaiser)
{
    _quickFixLoggerFactory = quickFixLoggerFactory;
    _eventsRaiser = eventsRaiser;
}
```

6. Substitute ILogFactory with IQuickFixLoggerFactory

```csharp
var settings = new QuickFix.SessionSettings(file);
var storeFactory = new QuickFix.FileStoreFactory(settings);
var initiator = new QuickFix.Transport.SocketInitiator(_tradeClientApp, storeFactory, settings, _quickFixLoggerFactory);
```

7. Subscribe to evnts, if you chose to use any

```csharp
_eventsRaiser.OnQuickFixEvent += (sender, e) => Console.WriteLine($"Event==>{e.MessageString}");
_eventsRaiser.OnQuickFixIncoming += (sender, e) => Console.WriteLine($"Incoming==>{e.MessageString}");
_eventsRaiser.OnQuickFixOutgoing += (sender, e) => Console.WriteLine($"Outgoing==>{e.MessageString}");
```

8. You may also use asyn event, setting AllowAsyncEvents property of QuickFixLoggerSettings to true

```csharp
_eventsRaiser.OnQuickFixEventAsync += _eventsRaiser_OnQuickFixEventAsync;
_eventsRaiser.OnQuickFixIncomingAsync += _eventsRaiser_OnQuickFixIncomingAsync;
_eventsRaiser.OnQuickFixOutgoingAsync += _eventsRaiser_OnQuickFixOutgoingAsync;

private async Task _eventsRaiser_OnQuickFixOutgoingAsync(object sender, QuickFixEventArgs e) => await Task.Run(() => { Console.WriteLine($"Outgoing==>{e.MessageString}"); });
private async Task _eventsRaiser_OnQuickFixIncomingAsync(object sender, QuickFixEventArgs e) => await Task.Run(() => { Console.WriteLine($"Incoming==>{e.MessageString}"); });
private async Task _eventsRaiser_OnQuickFixEventAsync(object sender, QuickFixEventArgs e) => await Task.Run(() => { Console.WriteLine($"Event==>{e.MessageString}"); });
```

## Settings

Property name | Property type | Description | Default value
--- | --- | --- | ---
LogMessages|bool|Specifies whether FIX messages should be logged|true
LogEvents|bool|Specifies whether FIX events should be logged|true
PrefixIncomingMessage|string|Specifies incoming FIX message prefix|\<incoming\>
PrefixOutgoingMessage|string|Specifies outgoing FIX message prefix|\<outgoing\>
PrefixEventMessage|string|Specifies FIX event prefix|\<event\>
ExcludedMsgTypes|Array of int values specifies message types which should not be logged|string[]|null
AllowAsyncEvents|bool|Specifies whether events should be raise asycronously|false
AllowRaisingForEvents|bool|Specifies whether OnEvent or OnEventAsync should be raised|false
AllowRaisingForIncoming|bool|Specifies whether OnIncoming or OnIncomingAsync should be raised|false
AllowRaisingForOutgoing|bool|Specifies whether OnOutgoing or OnOutgoingAsync should be raised|false

## Log examples

### Using default settings and Heartbit (0) message added to ExcludedMsgTypes

```txt
07/03/2023 14:29:10 [INF] <event> Created session
07/03/2023 14:29:22 [INF] <event> FIX.4.4:EXECUTOR->CLIENT1 Socket Reader 20974680 accepting session FIX.4.4:EXECUTOR->CLIENT1 from 127.0.0.1:55948
07/03/2023 14:29:22 [INF] <event> FIX.4.4:EXECUTOR->CLIENT1 Acceptor heartbeat set to 0 seconds
07/03/2023 14:29:22 [INF] <incoming> 8=FIX.4.49=7635=A34=149=CLIENT152=20230307-12:29:22.51856=EXECUTOR98=0108=30141=Y10=144
07/03/2023 14:29:22 [INF] <event> Sequence numbers reset due to ResetSeqNumFlag=Y
07/03/2023 14:29:22 [INF] <event> Session reset: Reset requested by counterparty
07/03/2023 14:29:22 [INF] <event> Session reset: ResetOnLogon
07/03/2023 14:29:22 [INF] <event> Received logon
07/03/2023 14:29:22 [INF] <outgoing> 8=FIX.4.49=7035=A34=149=EXECUTOR52=20230307-12:29:22.62856=CLIENT198=0108=3010=095
07/03/2023 14:29:22 [INF] <event> Responding to logon request
07/03/2023 14:30:22 [INF] <incoming> 8=FIX.4.49=14535=D34=349=CLIENT152=20230307-12:30:22.97856=EXECUTOR11=123456_78921=138=100040=354=255=EUR/ILS59=560=20230307-14:29:50.19499=1.03410=077
07/03/2023 14:30:40 [INF] <event> Session FIX.4.4:EXECUTOR->CLIENT1 disconnecting: Socket exception (127.0.0.1:55948): An existing connection was forcibly closed by the remote host.
07/03/2023 14:30:40 [INF] <event> Session reset: ResetOnDisconnect
```

### Using custom prefixes and Heartbit (0) message added to ExcludedMsgTypes

```txt
07/03/2023 14:29:22 [INF] FixEvent Created session
07/03/2023 14:29:22 [INF] FixEvent Connecting to 127.0.0.1 on port 5001
07/03/2023 14:29:22 [INF] FixEvent Connection succeeded
07/03/2023 14:29:22 [INF] FixEvent Session reset: ResetOnLogon
07/03/2023 14:29:22 [INF] FixEvent Session reset: ResetSeqNumFlag
07/03/2023 14:29:22 [INF] FixOutgoing 8=FIX.4.49=7635=A34=149=CLIENT152=20230307-12:29:22.51856=EXECUTOR98=0108=30141=Y10=144
07/03/2023 14:29:22 [INF] FixEvent Initiated logon request
07/03/2023 14:29:22 [INF] FixIncoming 8=FIX.4.49=7035=A34=149=EXECUTOR52=20230307-12:29:22.62856=CLIENT198=0108=3010=095
07/03/2023 14:29:22 [INF] FixEvent Received logon
07/03/2023 14:30:22 [INF] FixOutgoing 8=FIX.4.49=14535=D34=349=CLIENT152=20230307-12:30:22.97856=EXECUTOR11=123456_78921=138=100040=354=255=EUR/ILS59=560=20230307-14:29:50.19499=1.03410=077
```
