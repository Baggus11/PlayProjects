using System;
using System.Diagnostics.CodeAnalysis;

namespace Common
{
    public static class EventExtensions
    {
        /// 
        /// Raises an event thread-safely if the event has subscribers. 
        /// 
        /// The event handler to raise. 
        /// The object that sent this event. 
        ///args"> The event args. 
        [SuppressMessage("Microsoft.Design",
            "CA1030:UseEventsWhereAppropriate",
            Justification = "This warning comes up when you use the word `Fire` in a method name. This method specifically raises events, and so does not need changing.")]
        public static void Fire<T>(this EventHandler me, object sender, EventArgs args)
        {
            me?.Invoke(sender, args);
        }
        /// 
        /// Raises an event thread-safely if the event has subscribers. 
        /// 
        /// The type of EventArgs the event takes.
        /// The event handler to raise. 
        /// The object that sent this event. 
        /// The event args. 
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "This warning comes up when you use the word `Fire` in a method name. This method specifically raises events, and so does not need changing.")]
        public static void Fire<T>(this EventHandler me, object sender, T args) where T : EventArgs
        {
            me?.Invoke(sender, args);
        }
        /*For statics*/
        /// 
        /// Raises a static event thread-safely if the event has subscribers. 
        /// 
        /// The event handler to raise. 
        ///args"> The event args. 
        [SuppressMessage("Microsoft.Design",
            "CA1030:UseEventsWhereAppropriate",
            Justification = "This warning comes up when you use the word `Fire` in a method name. This method specifically raises events, and so does not need changing.")]
        public static void Fire<T>(this EventHandler me, EventArgs args)
        {
            me.Fire(null, args);
        }
        /// 
        /// Raises a static event thread-safely if the event has subscribers. 
        /// 
        /// The type of EventArgs the event takes.
        /// The event handler to raise. 
        /// The event args. 
        [SuppressMessage("Microsoft.Design",
            "CA1030:UseEventsWhereAppropriate",
            Justification = "This warning comes up when you use the word `Fire` in a method name. This method specifically raises events, and so does not need changing.")]
        public static void Fire<T>(this EventHandler me, T args) where T : EventArgs
        {
            me.Fire(null, args);
        }
    }
}
