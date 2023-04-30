using System;

namespace ag.QuickFix.Logger.Events
{
    /// <summary>
    /// Represents the class that contains event data
    /// </summary>
    public class QuickFixEventArgs : EventArgs
    {
        /// <summary>
        /// Gets event message string
        /// </summary>
        public string MessageString { get; }
        /// <summary>
        /// Creates new instance of <see cref="QuickFixEventArgs"/>
        /// </summary>
        /// <param name="messageString">Event message string</param>
        public QuickFixEventArgs( string messageString)
        {
            MessageString = messageString;
        }
    }
}
