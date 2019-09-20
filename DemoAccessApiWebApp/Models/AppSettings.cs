using System.Collections.Generic;

namespace DemoAccessApiWebApp.Models
{
    /// <summary>
    /// App Settings Class
    /// </summary>
    public class AppSettings
    {
        public DataAccessConfiguration DataAccessConfiguration { get; set; }
         
    }

    public class DataAccessConfiguration
    {       
        public string ApiFilms { get; set; }
      
    }
   
   
}
