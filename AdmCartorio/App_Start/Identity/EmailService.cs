using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace AdmCartorio.App_Start.Identity
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            //return ConfigSendGridasync(message);
            return SendMail(message);
        }

        // Implementação do SendGrid
        private Task ConfigSendGridasync(IdentityMessage message)
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("ADM_CARTORIO_SENDGRID_KEY");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(ConfigurationManager.AppSettings["ContaDeEmail"], "Adm Cartorio");
                var subject = message.Subject;
                var to = new EmailAddress(message.Destination, "Usuario Adm Cartorio");
                var plainTextContent = message.Body;
                var htmlContent = message.Body;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = client.SendEmailAsync(msg);

                return response;
            }
            catch (Exception)
            {
                return Task.FromResult(0);
            }
        }

        // Implementação de e-mail manual
        private Task SendMail(IdentityMessage message)
        {
            try
            {
                if (ConfigurationManager.AppSettings["EnviarEmailsIdentity"] == "true")
                {
                    var apiKey = Environment.GetEnvironmentVariable("ADM_CARTORIO_SENDGRID_KEY");
                    var client = new SendGridClient(apiKey);
                    var from = new EmailAddress(ConfigurationManager.AppSettings["ContaDeEmail"], "Adm Cartorio");
                    var subject = message.Subject;
                    var to = new EmailAddress(message.Destination, "Usuario Adm Cartorio");
                    var plainTextContent = message.Body;
                    var htmlContent = message.Body; 
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    var response = client.SendEmailAsync(msg);
                    return  response;
                }
            }
            catch (Exception)
            {
                //return Task..FromException(ex);
            }

            return Task.FromResult(0);
        }
    }
}