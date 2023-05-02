using ag.QuickFix.Logger.Events;
using ag.QuickFix.Logger.Extensions;
using System;
using System.Threading.Tasks;

namespace ag.QuickFix.Logger
{
    internal class QuickFixEventsRaiser : IQuickFixEventsRaiser
    {
        public event AsyncEventHandler<QuickFixEventArgs> OnQuickFixEventAsync;
        public event AsyncEventHandler<QuickFixEventArgs> OnQuickFixIncomingAsync;
        public event AsyncEventHandler<QuickFixEventArgs> OnQuickFixOutgoingAsync;
        public event EventHandler<QuickFixEventArgs> OnQuickFixEvent;
        public event EventHandler<QuickFixEventArgs> OnQuickFixIncoming;
        public event EventHandler<QuickFixEventArgs> OnQuickFixOutgoing;

        public void OnEvent(object sender, QuickFixEventArgs e) => OnQuickFixEvent?.Invoke(sender, e);
        public async Task OnEventAsync(object sender, QuickFixEventArgs e)
        {
            if (OnQuickFixEventAsync != null)
            {
                await OnQuickFixEventAsync.InvokeAsync(sender, e);
            }
        }
        public void OnIncoming(object sender, QuickFixEventArgs e) => OnQuickFixIncoming?.Invoke(sender, e);
        public async Task OnIncomingAsync(object sender, QuickFixEventArgs e)
        {
            if (OnQuickFixIncomingAsync != null)
            {
                await OnQuickFixIncomingAsync.InvokeAsync(sender, e);
            }
        }
        public void OnOutgoing(object sender, QuickFixEventArgs e) => OnQuickFixOutgoing?.Invoke(sender, e);
        public async Task OnOutgoingAsync(object sender, QuickFixEventArgs e)
        {
            if (OnQuickFixOutgoingAsync != null)
            {
                await OnQuickFixOutgoingAsync.InvokeAsync(sender, e);
            }
        }
    }
}
