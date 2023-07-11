using MelonBookshelfApi.Services.Contracts;
using MimeKit;
using System.Net;
using System.Net.Mail;

namespace MelonBookshelfApi.Services
{
    public class EmailSender : IMessageSender
    {
        private readonly IConfiguration _config;
        public EmailSender(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendMessage(string to, string content)
        {
            var mail = new MailMessage();
            var SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("melonbookshelf@gmail.com");
            mail.To.Add(to);
            mail.Subject = content.Split()[0];
            mail.Body = content;

            SmtpServer.Port = 465;
            SmtpServer.Credentials = new NetworkCredential("davidpetkov2006@gmail.com", "rmwsxzhaikdjxusa");
            SmtpServer.EnableSsl = true;

            await SmtpServer.SendMailAsync(mail);
        }
    }
}
