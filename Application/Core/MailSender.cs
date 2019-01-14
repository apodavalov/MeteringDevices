using System.Net;
using System.Net.Mail;
using System;

namespace MeteringDevices.Core
{
    class MailSender : IMessageSender
    {
        private readonly string _Host;
        private readonly int _Port;
        private readonly bool _EnableSsl;
        private readonly string _FromAddress;
        private readonly string _Password;
        private readonly string _ToAddress;
        private readonly string _Subject;

        public MailSender(string host, int port, bool enableSsl, string fromAddress, string password, string toAddress, string subject)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            if (port < 0 || port > 65535)
            {
                throw new ArgumentOutOfRangeException(nameof(port), port, "Expects value between 0 and 65535.");
            }

            if (fromAddress == null)
            {
                throw new ArgumentNullException(nameof(fromAddress));
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (toAddress == null)
            {
                throw new ArgumentNullException(nameof(toAddress));
            }

            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            _Host = host;
            _Port = port;
            _EnableSsl = enableSsl;
            _FromAddress = fromAddress;
            _Password = password;
            _ToAddress = toAddress;
            _Subject = subject;
        }

        public void Send(string message)
        {
            MailAddress fromAddress = new MailAddress(_FromAddress);
            MailAddress toAddress = new MailAddress(_ToAddress);

            using (SmtpClient smtpClient = new SmtpClient
            {
                Host = _Host,
                Port = _Port,
                EnableSsl = _EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_FromAddress, _Password)
            })
            {

                using (MailMessage mailMessage = new MailMessage(_FromAddress, _ToAddress))
                {
                    mailMessage.Subject = _Subject;
                    mailMessage.Body = message;
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}
