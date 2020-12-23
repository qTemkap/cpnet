using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Task4SMTP
{
    class Program
    {
        static void Main(string[] args)
        {
            sendMail("smtp.gmail.com", "smtp837@gmail.com", "kwfvtuypcjabbrui", "artemka.relit@gmail.com", "Тема письма", "Тестовая оправка сообщени для проверки работы SMTP ");
            Console.WriteLine("Сообщение отправлено");
            Console.Read();
        }
        public static void sendMail(string smtpServer, string from, string password, string mailto, string caption, string message, string attachFile = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.Subject = caption;
                mail.To.Add(new MailAddress(mailto));                
                mail.Body = message;
                if (!string.IsNullOrEmpty(attachFile))
                    mail.Attachments.Add(new Attachment(attachFile));
                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;               
                client.Credentials = new NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Port = 587;
                client.EnableSsl = true;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }
    }
}
