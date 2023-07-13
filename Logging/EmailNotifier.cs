using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using Serilog;
using System.Configuration;

namespace Logging
{
    public class EmailNotifier
    {
        public string Sender { private get; set; }
        public string SenderDisplayName { private get; set; }
        public string Recipient { private get; set; }
        public string Subject { private get; set; } = "PDF report - Release Manager";
        public string Body { private get; set; } = "The latest PDF report generated after the check is enclosed.";
        public MailMessage Message { private get; set; }
        public Attachment EmailAttachment { private get; set; }
        public string SmtpHost { private get; set; }
        public string Status { get; set; }

        private readonly ILogger _logger = new SerilogClass().logger;
        Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public EmailNotifier(string sender, string senderDisplayName, string recipient, Attachment attachment) 
        {
            Sender = sender;
            SenderDisplayName = senderDisplayName;
            Recipient = recipient;
            EmailAttachment = attachment;
            SmtpHost = configFile.AppSettings.Settings["smtphost"].Value;

            FormEmail();
            SendEmail();
        }


        private void FormEmail()
        {
            try
            {
                MailMessage email = new MailMessage();
                
                email.From = new MailAddress(Sender, SenderDisplayName);
                email.To.Add(Recipient);
                email.Subject = Subject;
                email.Body = Body;
                email.Attachments.Add(EmailAttachment);
                Message = email;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occurred while preparing an e-mail with attachment. See error below:\r\n {ex.Message}");
            }
        }

        private void SendEmail()
        {
            try
            {
                SmtpClient client = new SmtpClient(SmtpHost);
                client.Send(Message);
                _logger.Debug($"E-mail has been sent to: {Message.To}.");
                Status = $"E-mail with PDF report has been sent to {Message.To}";
            }
            catch (SmtpException ex)
            {
                _logger.Error($"Smtp-related error occurred. See error below:\r\n {ex.Message}");
                Status = $"ERROR: E-mail could not have been sent due to error.";
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occurred. See error below:\r\n {ex.Message}");
                Status = $"ERROR:E-mail could not have been sent due to error. See log for more details.";
            }
        }
    }
    
}
