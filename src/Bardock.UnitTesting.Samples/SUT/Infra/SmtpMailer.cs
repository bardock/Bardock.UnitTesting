namespace Bardock.UnitTesting.Samples.SUT.Infra
{
    public class SmtpMailer : IMailer
    {
        public void Send(string address, string body)
        {
            // Send mails with SMTP
        }
    }
}