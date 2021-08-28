using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DotnetEmail.Services
{
    public interface IEmailService
    {
        public Task<bool> SendMailAsync(string from, string to, string subject, string templateFilePath, dynamic model, string sendGridKey);
    }
}