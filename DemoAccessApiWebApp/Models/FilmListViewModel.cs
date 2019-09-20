using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAccessApiWebApp.Models
{
    public class FilmListViewModel
    {
        public string PageHeader { get; set; }
        public List<FilmViewModel> Films { get; set; }
    }

    public class FilmViewModel
    {      
        public string Title { get; set; }
        public long EpisodeId { get; set; }

        [JsonProperty("opening_crawl")]
        public string OpeningCrawl { get; set; }
      
        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }

        public string Url { get; set; }
        
    }


}
