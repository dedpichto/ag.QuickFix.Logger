using ag.QuickFix.Logger.Events;
using ag.QuickFix.Logger.Extensions;
using System;
using System.Threading.Tasks;

namespace ag.QuickFix.Logger
{
    /// <summary>
    /// Public interface for QuickFixEventsRaiser
    /// </summary>
    public interface IQuickFixEventsRaiser
    {
        /// <summary>
        /// Async event raised when QuickFix OnEvent occurs
        /// </summary>
        event AsyncEventHandler<QuickFixEventArgs> OnQuickFixEventAsync;
        /// <summary>
        /// Async event raised when QuickFix OnIncoming occurs
        /// </summary>
        event AsyncEventHandler<QuickFixEventArgs> OnQuickFixIncomingAsync;
        /// <summary>
        /// Async event raised when QuickFix OnOutgoing occurs
        /// </summary>
        event AsyncEventHandler<QuickFixEventArgs> OnQuickFixOutgoingAsync;
        /// <summary>
        /// Event raised when QuickFix OnEvent occurs
        /// </summary>
        event EventHandler<QuickFixEventArgs> OnQuickFixEvent;
        /// <summary>
        /// Event raised when QuickFix OnIncoming occurs
        /// </summary>
        event EventHandler<QuickFixEventArgs> OnQuickFixIncoming;
        /// <summary>
        /// Event raised when QuickFix OnOutgoing occurs
        /// </summary>
        event EventHandler<QuickFixEventArgs> OnQuickFixOutgoing;
        /// <summary>
        /// Raises <see cref="OnQuickFixEvent"/>
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Agrument of type<see cref="QuickFixEventArgs"/></param>
        void OnEvent(object sender, QuickFixEventArgs e);
        /// <summary>
        /// Raises <see cref="OnQuickFixIncoming"/>
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Agrument of type<see cref="QuickFixEventArgs"/></param>
        void OnIncoming(object sender, QuickFixEventArgs e);
        /// <summary>
        /// Raises <see cref="OnQuickFixOutgoing"/>
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Agrument of type<see cref="QuickFixEventArgs"/></param>
        void OnOutgoing(object sender, QuickFixEventArgs e);
        /// <summary>
        /// Raises <see cref="OnQuickFixEventAsync"/>
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Agrument of type<see cref="QuickFixEventArgs"/></param>
        /// <returns>Task</returns>
        Task OnEventAsync(object sender, QuickFixEventArgs e);
        /// <summary>
        /// Raises <see cref="OnQuickFixIncomingAsync"/>
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Agrument of type<see cref="QuickFixEventArgs"/></param>
        /// <returns>Task</returns>
        Task OnIncomingAsync(object sender, QuickFixEventArgs e);
        /// <summary>
        /// Raises <see cref="OnQuickFixOutgoingAsync"/>
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Agrument of type<see cref="QuickFixEventArgs"/></param>
        /// <returns>Task</returns>
        Task OnOutgoingAsync(object sender, QuickFixEventArgs e);
    }
}
