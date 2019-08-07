/*----------------------------------------------------------------------------
  _____            _                    _____        __ _   
/  __ \          | |                  /  ___|      / _| |  
| /  \/ __ _ _ __| |_ ___   ___  _ __ \ `--.  ___ | |_| |_ 
| |    / _` | '__| __/ _ \ / _ \| '_ \ `--. \/ _ \|  _| __|
| \__/\ (_| | |  | || (_) | (_) | | | /\__/ / (_) | | | |_ 
 \____/\__,_|_|   \__\___/ \___/|_| |_\____/ \___/|_|  \__|
Todos os direitos reservados ®                       
-----------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Core.Repositories
{
    public class InfraDataEventLogging
    {
        private string Application;
        private string EventLogName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="log"></param>
        public InfraDataEventLogging(string app, string log)
        {
            Application = app;
            EventLogName = log;

            // Create the event log if it doesn't exist
            if (!EventLog.SourceExists(Application))
            {
                EventLog.CreateEventSource(Application, EventLogName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        public void WriteToEventLog(string message, string type)
        {
            switch (type.ToUpper())
            {
                case "INFO":
                    EventLog.WriteEntry(Application, message, EventLogEntryType.Information);
                    break;
                case "ERROR":
                    EventLog.WriteEntry(Application, message, EventLogEntryType.Error);
                    break;
                case "WARN":
                    EventLog.WriteEntry(Application, message, EventLogEntryType.Warning);
                    break;
                default:
                    EventLog.WriteEntry(Application, message, EventLogEntryType.Information);
                    break;
            }
        }

    }
}
