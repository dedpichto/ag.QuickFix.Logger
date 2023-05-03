namespace ag.QuickFix.Logger.Settings
{
    /// <summary>
    /// Represents a <see cref="Logger"/> settings.
    /// </summary>
    public class QuickFixLoggerSettings
    {
        /// <summary>
        /// Specifies whether FIX messages should be logged.
        /// </summary>
        public bool LogMessages { get; set; } = true;
        /// <summary>
        /// Specifies whether FIX events should be logged.
        /// </summary>
        public bool LogEvents { get; set; } = true;
        /// <summary>
        /// Specifies incoming FIX message prefix.
        /// </summary>
        public string PrefixIncomingMessage { get; set; } = "<incoming>";
        /// <summary>
        /// Specifies outgoing FIX message prefix.
        /// </summary>
        public string PrefixOutgoingMessage { get; set; } = "<outgoing>";
        /// <summary>
        /// Specifies FIX event prefix.
        /// </summary>
        public string PrefixEventMessage { get; set; } = "<event>";
        /// <summary>
        /// Array of int values specifies message types which should not be logged.
        /// </summary>
        public string[] ExcludedMsgTypes { get; set; }
        /// <summary>
        /// Specifies whether events should be raise asycronously
        /// </summary>
        public bool AllowAsyncEvents { get; set; }
        /// <summary>
        /// Specifies whether <see cref="QuickFixEventsRaiser.OnEvent"/> or <see cref="QuickFixEventsRaiser.OnEventAsync"/> should be raised
        /// </summary>
        public bool AllowRaisingForEvents { get; set; }
        /// <summary>
        /// Specifies whether <see cref="QuickFixEventsRaiser.OnIncoming"/> or <see cref="QuickFixEventsRaiser.OnIncomingAsync"/> should be raised
        /// </summary>
        public bool AllowRaisingForIncoming { get; set; }
        /// <summary>
        /// Specifies whether <see cref="QuickFixEventsRaiser.OnOutgoing"/> or <see cref="QuickFixEventsRaiser.OnOutgoingAsync"/> should be raised
        /// </summary>
        public bool AllowRaisingForOutgoing { get; set; }

    }
}
