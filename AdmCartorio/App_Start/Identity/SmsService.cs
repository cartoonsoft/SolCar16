using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AdmCartorio.App_Start.Identity
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            if (ConfigurationManager.AppSettings["Internet"] == "true")
            {
                // Utilizando TWILIO como SMS Provider.
                // https://www.twilio.com/docs/quickstart/csharp/sms/sending-via-rest

                const string accountSid = "SEU ID";
                const string authToken = "SEU TOKEN";

                //to-do: ronaldo fazer
                //var client = new TwilioRestClient(accountSid, authToken);
                //client.SendMessage("814-350-7742", message.Destination, message.Body);
            }

            return Task.FromResult(0);
        }
    }
}