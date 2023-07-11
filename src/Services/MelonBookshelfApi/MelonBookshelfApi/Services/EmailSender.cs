using MailKit.Net.Smtp;
using MelonBookshelfApi.Services.Contracts;
using MimeKit;

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
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("Melon Bookshelf", "melonbookshelf@gmail.com"));
			message.To.Add(new MailboxAddress("", to));
			message.Subject = content.Split()[0];

			message.Body = new TextPart("plain")
			{
				Text = content
			};

			using var client = new SmtpClient();

			await client.ConnectAsync("smtp.gmail.com", 465, true);

			await client.AuthenticateAsync("davidpetkov2006@gmail.com", "rmwsxzhaikdjxusa");

			await client.SendAsync(message);
			await client.DisconnectAsync(true);
		}
    }
}
