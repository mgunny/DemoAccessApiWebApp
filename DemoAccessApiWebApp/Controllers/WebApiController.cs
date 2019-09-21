using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using DataAccess.Repository;
using DemoAccessApiWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoAccessApiWebApp.Controllers
{
    [Route("api/[controller]")]
    public class WebApiController : Controller
    {

        private readonly IDataRepository _dataRepository;
        private readonly AppSettings _appSettings;

        public WebApiController(IDataRepository dataRepository, IOptions<AppSettings> appSettings)
        {
            _dataRepository = dataRepository;
            _appSettings = appSettings.Value;
        }

        // GET: api/<controller>
        [HttpGet("Films")]
        public async Task<IActionResult> AllFilms()
        {
            try
            {                
                // Get the Data
                var dataResponse = await _dataRepository.GetFilmsAsync();

                if (dataResponse.StatusCode == HttpStatusCode.OK)
                {
                    var filmData = dataResponse.ApiData;
                    var films = Mapper.Map(filmData.Results.OrderBy(f => f.ReleaseDate).ToList()).ToANew<List<FilmViewModel>>();
                    return Ok(films);
                }
                else
                {
                    return BadRequest("Unable to contact API");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet("Films/{id}")]
        public string Get(int id)
        {
            return "value";
        }

      
    }
}
