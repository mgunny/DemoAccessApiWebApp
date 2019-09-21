using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoAccessApiWebApp.Models;
using DataAccess.Repository;
using System.Net;
using AgileObjects.AgileMapper;

namespace DemoAccessApiWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataRepository _dataRepository;

        // Contstructor: Use Dependency Injection to inject an instance of the Data Repository
        public HomeController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [ResponseCache(Duration = 30, VaryByQueryKeys = new string[] { "*" })]
        public async Task<IActionResult> Index()
        {

            // Create a default View Model
            FilmListViewModel model = new FilmListViewModel { PageHeader = string.Empty, Films = new List<FilmViewModel>() };

            try
            {
                // Get the API data from Repository
                var dataResponse = await _dataRepository.GetFilmsAsync();

                // Populate view model with films in release year order - if response is ok
                //   - Map incoming data to View Model - could do this manually by assigning each property but quicker to use mapper plugin
                if (dataResponse.StatusCode == HttpStatusCode.OK)
                {
                    var filmData = dataResponse.ApiData;
                    model.Films = Mapper.Map(filmData.Results.OrderBy(f => f.ReleaseDate).ToList()).ToANew<List<FilmViewModel>>();
                    model.PageHeader = $"There are {filmData.Count} films returned from the API";
                }
                else
                {
                    model.PageHeader = "There was an issue contacting the Films API";
                }
            }
            catch (Exception ex)
            {
                model.PageHeader = $"An error has occurred: {ex.Message}";

                // Log the error here - not implemented in this test.
            }

            return View(model);
        }
     
        [ResponseCache(Duration = 30, VaryByQueryKeys = new string[] { "*" })]
        public async Task<IActionResult> Detail(int id)
        {
            // Create a default View Model
            FilmDetailViewModel model = new FilmDetailViewModel();

            try
            {                
                // Get the API data response from Repository
                var dataResponse = await _dataRepository.GetFilmDetailsAsync(id);

                // Populate view model with films in release year order - if response is ok
                if (dataResponse.StatusCode == HttpStatusCode.OK)
                {
                    var filmData = dataResponse.ApiData;

                    // Map incoming data to View Model - could do this manually by assigning each property but quicker to use mapper plugin and then assign specific props as reqd.
                    model = Mapper.Map(filmData).ToANew<FilmDetailViewModel>();

                    model.PageHeader = $"Details for: {model.Title}";
                    model.PlanetsCount = filmData.Planets.Count;
                    model.StarshipsCount = filmData.Starships.Count;
                    model.CharactersCount = filmData.Characters.Count;
                }
                else
                {
                    model.PageHeader = "Film Details not Found";
                }
            }
            catch (Exception ex)
            {
                model.PageHeader = $"An error has occurred: {ex.Message}";

                // Log the error here - not implemented in this test.
            }

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
