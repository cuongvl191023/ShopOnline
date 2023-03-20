using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using ShopOnline.Models;

namespace ShopOnline.Service
{
    public class SendMailService : IEmailSender
    {
        private readonly MailSettings mailSettings;

        private readonly ILogger<SendMailService> logger;

        public SendMailService(IOptions<MailSettings> _mailSettings, ILogger<SendMailService> _logger)
        {
            mailSettings = _mailSettings.Value;
            logger = _logger;
            logger.LogInformation("Create SendMailService");
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MimeMessage message = new()
            {
                Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail)
            };
            message.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            BodyBuilder builder = new()
            {
                HtmlBody = htmlMessage
            };
            message.Body = builder.ToMessageBody();
            using MailKit.Net.Smtp.SmtpClient smtp = new();
            try
            {
                smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                _ = await smtp.SendAsync(message);
            }
            catch (Exception ex)
            {
                _ = System.IO.Directory.CreateDirectory("mailssave");
                string emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await message.WriteToAsync(emailsavefile);
                logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailsavefile);
                logger.LogError(ex.Message);
            }
            smtp.Disconnect(true);
            logger.LogInformation("send mail to: " + email);

        }
    }
}