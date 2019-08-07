using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Infra.Cross.Identity.Configuration
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Utilizando TWILIO como SMS Provider.
            // https://www.twilio.com/docs/quickstart/csharp/sms/sending-via-rest

            string accountSid = Environment.GetEnvironmentVariable("CARTORIO11RI_TWILIO_SID");
            string authToken = Environment.GetEnvironmentVariable("CARTORIO11RI_TWILIO_TOKEN");
            TwilioClient.Init(accountSid, authToken);

            var messagem = MessageResource.Create(
                body: message.Body,
                from: new Twilio.Types.PhoneNumber("++12562570151"),
                to: new Twilio.Types.PhoneNumber(message.Destination)
            );

            return Task.FromResult(0);
        }
    }
}