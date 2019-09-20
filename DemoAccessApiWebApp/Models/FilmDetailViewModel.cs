using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAccessApiWebApp.Models
{
    public class FilmDetailViewModel
    {
        public string PageHeader { get; set; }

        public string Title { get; set; }
        public long EpisodeId { get; set; }

        [JsonProperty("opening_crawl")]
        public string OpeningCrawl { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }

        public int PlanetsCount { get; set; }
        public int StarshipsCount { get; set; }
        public int CharactersCount { get; set; }

    }
}
