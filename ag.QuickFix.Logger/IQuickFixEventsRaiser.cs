using ag.QuickFix.Logger.Events;
using System;

namespace ag.QuickFix.Logger
{
    /// <summary>
    /// Public interface for QuickFixEventsRaiser
    /// </summary>
    public interface IQuickFixEventsRaiser
    {
        //event AsyncEventHandler<QuickFixEventArgs> OnQuickFixEventAsync;
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
        //Task RaiseEventAsync(object sender, QuickFixEventArgs e);
    }
}
