

namespace API.Helpers
{


    namespace API.Helpers
    {
        
         using MimeKit;
        using MailKit.Net.Smtp;
       

        public class MailHelper
        {
            public async void SendMail(string to, string subject, string body)
            {

                var from = "mirealponce1407@gmail.com";
                var smtp = "smtp.gmail.com";
                var port = "587";
                var password = "Mireal422090";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(from));
                message.To.Add(new MailboxAddress(to));
                message.Subject = subject;
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = body;
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(smtp, int.Parse(port), false);
                    client.Authenticate(from, password);
                    client.Send(message);
                    client.Disconnect(true);

                }

            }
        }
    }
}