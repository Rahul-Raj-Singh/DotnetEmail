using System;
using System.IO;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Razor;
using FluentEmail.SendGrid;
using Microsoft.Extensions.Configuration;

namespace DotnetEmail.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendMailAsync(string from, string to, string subject, string templateFilePath, dynamic model, string sendGridKey)
        {
            try
            {
                var email = 
                    new Email(renderer: new RazorRenderer(), sender: new SendGridSender(apiKey: sendGridKey))
                    .SetFrom(from)
                    .To(to)
                    .Subject(subject)
                    .UsingTemplateFromFile<dynamic>(filename: templateFilePath, model: null, isHtml: true);
    
                var res = await email.SendAsync();

                if(res.Successful) return true;

                LogErrors(res, null); return false;
                
            }
            catch(Exception e)
            {
                LogErrors(null, e);
                return false;
            }

        }

        private void LogErrors(SendResponse res = null, Exception e = null)
        {
            Console.WriteLine("Error Occured Sending Mail");

            if(e != null)
            {
                Console.WriteLine(e);
            }

            if(res != null)
            {
                Console.WriteLine("Message Id:" + res.MessageId);
                Console.WriteLine(string.Join("\n", res.ErrorMessages));
            }
        }
    }
}