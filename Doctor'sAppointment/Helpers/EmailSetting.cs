using System.Net.Mail;
using System.Net;
using DAL.Models;

namespace Doctor_sAppointment.Helpers
{
    public static class EmailSitting
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("kamalahmed271@gmail.com", "bxmgxhdbmmcpftaz");
            Client.Send("kamalahmed271@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
