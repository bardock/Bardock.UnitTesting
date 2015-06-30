namespace Bardock.UnitTesting.Samples.SUT.Infra
{
    public interface IMailer
    {
        void Send(string address, string body);
    }
}