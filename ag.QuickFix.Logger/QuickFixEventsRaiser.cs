using ag.QuickFix.Logger.Events;
using System;

namespace ag.QuickFix.Logger
{
    internal class QuickFixEventsRaiser : IQuickFixEventsRaiser
    {
        public event EventHandler<QuickFixEventArgs> OnQuickFixEvent;
        public event EventHandler<QuickFixEventArgs> OnQuickFixIncoming;
        public event EventHandler<QuickFixEventArgs> OnQuickFixOutgoing;

        public void OnEvent(object sender, QuickFixEventArgs e) => OnQuickFixEvent?.Invoke(sender, e);
        public void OnIncoming(object sender, QuickFixEventArgs e) => OnQuickFixIncoming?.Invoke(sender, e);
        public void OnOutgoing(object sender, QuickFixEventArgs e) => OnQuickFixOutgoing?.Invoke(sender, e);
    }
}
