using DemoAccessApiWebApp.Models;
using DemoAccessApiWebApp.Services;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public partial class DataRepository : IDataRepository
    {
        private readonly IAPIService _apiService;
        private readonly AppSettings _appSettings;

        public DataRepository(IAPIService apiService, IOptions<AppSettings> appSettings)
        {
            _apiService = apiService;
            _appSettings = appSettings.Value;
        }        

        /// <summary>
        /// Get All Films
        /// </summary>
        public async Task<ApiHttpResponse<FilmData>> GetFilmsAsync()
        {
            string url = _appSettings.DataAccessConfiguration.ApiFilms;
            return await _apiService.GetDataAsync<FilmData>(url);
        }

        /// <summary>
        /// Get Specific Film Details from Id
        /// </summary>      
        public async Task<ApiHttpResponse<Film>> GetFilmDetailsAsync(int id)
        {
            string url = $"{_appSettings.DataAccessConfiguration.ApiFilms}{id}";
            return await _apiService.GetDataAsync<Film>(url);
        }


    }


}
