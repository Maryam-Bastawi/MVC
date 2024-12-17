using company.ass.DAL.models;
using System.Net;
using System.Net.Mail;

namespace company.ass.pl.helpers
{
    public static class EmailSetting
    {
        public static void SendEmail(Email email)
        {
            //mail server : gmail.com
            //smtp
            var client = new SmtpClient("smtp.gmail.com" , 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("MaryamBastawi1@gmail.com", "oxxvpyxrbvbeehkh");
            client.Send("MaryamBastawi1@gmail.com", email.To , email.Subjict , email.Body);

		}
	}
}
