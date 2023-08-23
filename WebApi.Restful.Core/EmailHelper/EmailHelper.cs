using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Restful.Core.Configuration;
using WebApiRestful.Domain.Model;

namespace WebApi.Restful.Core.EmailHelper
{
    public class EmailHelper : IEmailHelper
    {
        EmailConfig _emailConfig;

        public EmailHelper(IOptions<EmailConfig> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task SendEmailAsync(CancellationToken cancellationToken, EmailRequest emailRequest)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(_emailConfig.Provider, _emailConfig.Port);
                smtpClient.Credentials = new NetworkCredential(_emailConfig.DefaultSender, _emailConfig.Password);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_emailConfig.DefaultSender);
                mailMessage.To.Add(emailRequest.To);
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = emailRequest.Subject;
                mailMessage.Body = emailRequest.Content;

                if (emailRequest.AttachmentFilePaths.Length > 0)
                {
                    foreach (var path in emailRequest.AttachmentFilePaths)
                    {
                        Attachment attachment = new Attachment(path);

                        mailMessage.Attachments.Add(attachment);
                    }
                }

                await smtpClient.SendMailAsync(mailMessage, cancellationToken);

                mailMessage.Dispose();
            }
            catch (Exception ex)
            {
                //log ex
                throw;
            }

        }

    }
}
