using System.Collections.Generic;
using System.Threading.Tasks;
using DemoAccessApiWebApp.Models;

namespace DataAccess.Repository
{
    public interface IDataRepository
    {
        Task<ApiHttpResponse<FilmData>> GetFilmsAsync();
        Task<ApiHttpResponse<Film>> GetFilmDetailsAsync(int id);
    }
}