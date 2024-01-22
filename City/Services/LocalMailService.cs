using System.Text;

namespace City.Services
{
    public class LocalMailService : ILocalMailService
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = configuration["MailSettings:mailTo"];
            _mailFrom  = configuration["MailSettings:mailFrom"];
        }
        public void Send(string subject, string message)
        {
            StringBuilder showMail = new StringBuilder();
            showMail.AppendLine($"Received To : {_mailTo}");
            showMail.AppendLine($"Sent By : {_mailFrom}");
            showMail.AppendLine("-------------------------------------------");
            showMail.AppendLine($"Subject : {subject}");
            showMail.AppendLine($"Message : {message}");
            showMail.AppendLine("-------------------------------------------");
            showMail.AppendLine($"TimeStamp - {DateTime.Now}");
            showMail.AppendLine($"MailService : {nameof(LocalMailService)}");

            Console.WriteLine(showMail.ToString());
        }
    }
}
