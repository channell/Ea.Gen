using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.Gen.Addin
{
    /// <summary>
    /// Log to the event log
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Log the Exception
        /// </summary>
        /// <param name="e"></param>
        /// <param name="format"></param>
        /// <param name="pam"></param>
        public static void Error(Exception e, string format, string pam)
        {
            var m = string.Format(format, pam) + "\n\n" + e.Message + "\n" + e.StackTrace;
            EventLog.WriteEntry("EA.Gen.Addin", m, EventLogEntryType.Error);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pam"></param>
        public static void Error(string pam)
        {
            EventLog.WriteEntry("EA.Gen.Addin", pam, EventLogEntryType.Error);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="format"></param>
        /// <param name="pam"></param>
        public static void Warning(Exception e, string format, string pam)
        {
            var m = string.Format(format, pam) + "\n\n" + e.Message + "\n" + e.StackTrace;
            EventLog.WriteEntry("EA.Gen.Addin", m, EventLogEntryType.Warning);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="pam"></param>
        public static void Warning(Exception e, string pam)
        {
            var m = pam + "\n\n" + e.Message + "\n" + e.StackTrace;
            EventLog.WriteEntry("EA.Gen.Addin", m, EventLogEntryType.Warning);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="pam"></param>
        public static void Information(string format, string pam)
        {
            var m = string.Format(format, pam);
            EventLog.WriteEntry("EA.Gen.Addin", m, EventLogEntryType.Information);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pam"></param>
        public static void Information(string pam)
        {
            EventLog.WriteEntry("EA.Gen.Addin", pam, EventLogEntryType.Information);
        }
    }
}
