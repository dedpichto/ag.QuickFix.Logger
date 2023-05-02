using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ag.QuickFix.Logger.Extensions
{
    /// <summary>
    /// Handles async events
    /// </summary>
    /// <typeparam name="QuickFixEventArgs"></typeparam>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns>Task</returns>
    public delegate Task AsyncEventHandler<QuickFixEventArgs>(
        object sender, QuickFixEventArgs e);

    internal static class AsynEventHandlerExtensions
    {
        public static async Task InvokeAsync<QuickFixEventArgs>(
            this AsyncEventHandler<QuickFixEventArgs> handler,
            object sender, QuickFixEventArgs args)
        {
            await Task.Run(async () =>
            {
                var delegates = handler?.GetInvocationList();
                if (delegates?.Length > 0)
                {
                    var tasks =
                    delegates
                    .Cast<AsyncEventHandler<QuickFixEventArgs>>()
                    .Select(e => Task.Run(
                        async () => await e.Invoke(sender, args)));
                    await Task.WhenAll(tasks);
                }
            }).ConfigureAwait(false);
        }
    }
}
