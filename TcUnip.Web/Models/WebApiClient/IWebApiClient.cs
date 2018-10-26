using System.Threading.Tasks;

namespace TcUnip.Web.WebApiClient
{
    public interface IWebApiClient
    {
        IWebApiClient SetBearerAuthentication(string authToken);
        IWebApiClient SetBasicAuthentication();
        string baseUri { get; set; }
        //Task<T> AuthenticateAsync<T>(string userName, string password);
        Task<T> GetAsync<T>(string action);
        void GetNoWait(string action);
        Task PutAsync<T>(string action, T data);
        Task PostAsync<T>(string action, T data);
        Task<Tout> PostWithReturnAsync<Tin, Tout>(string action, Tin data);
        //Task<T> AuthenticateAsync<T>(string refreshToken);
        Task<T> DeleteAsync<T>(string action);
    }
}
