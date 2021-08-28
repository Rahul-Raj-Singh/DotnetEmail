using System.IO;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using FluentEmail.Core;
using DotnetEmail.Services;
using System.Threading.Tasks;

namespace DotnetEmail
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Build Config
            var config = new ConfigurationBuilder()
                                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
                                .Build();
            
            // Send Mail
            var emailService = await new EmailService().SendMailAsync(
                from: config["From"],
                to: config["To"],
                subject: "Game Price Drop 😃",
                templateFilePath: Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplate.cshtml"),
                model: null,
                sendGridKey: config["SendGridAPIKey"]
            );

        }
    }
}
