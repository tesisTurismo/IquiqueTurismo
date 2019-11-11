using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
//using System.Web.Configuration;

namespace appTurismoIqq.Helpers
{
    public class MailHelper
    {
        Boolean estado=true;
        string merror;
        public static async Task SendMail(string destinatario, string subject, string body)
        { 

        var message = new MailMessage();
         message.To.Add(destinatario);
         message.From= new MailAddress("lucio_iron_man@hotmail.com","Administrador AppTurismo Iquique");
        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml= true;
            message.Priority = MailPriority.Normal;
       

            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.live.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("lucio_iron_man@hotmail.com", "sxa316q52462");
                smtp.Send(message);
            }
            catch (SmtpException error)
            {

                 Console.Write(error.Message);
            }



        }
       // public static async Task SendMail(List<string> mails, string subject, string body)
     }
    
}
