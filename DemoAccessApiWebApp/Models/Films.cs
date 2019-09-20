using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAccessApiWebApp.Models
{

    public class FilmData
    {
        public long Count { get; set; }
     
        public List<Film> Results { get; set; }
    }

    public class Film
    {
        public string Title { get; set; }
        public long EpisodeId { get; set; }

        [JsonProperty("opening_crawl")]
        public string OpeningCrawl { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }
        public List<Uri> Characters { get; set; }
        public List<Uri> Planets { get; set; }
        public List<Uri> Starships { get; set; }
              
        public string Url { get; set; }
    }


}
