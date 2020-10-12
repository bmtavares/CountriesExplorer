namespace CountriesExplorer.Common.Services
{
    using System.Threading.Tasks;

    using CountriesExplorer.Common.Models;

    public interface IApiService
    {
        Task<Response> GetCountries(string urlBase, string controller);

        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
    }
}
