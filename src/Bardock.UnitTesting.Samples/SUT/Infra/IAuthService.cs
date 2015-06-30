namespace Bardock.UnitTesting.Samples.SUT.Infra
{
    public interface IAuthService
    {
        void Authorize(string username, string resource);
    }
}