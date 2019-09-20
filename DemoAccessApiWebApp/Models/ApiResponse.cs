using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DemoAccessApiWebApp.Models
{
    
    // Response with no Payload data
    public class ApiHttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public ApiResponse ErrorApiResponse { get; set; }
    }

    // Response with Payload data
    public class ApiHttpResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public ApiResponse ErrorApiResponse { get; set; }
        public T ApiData { get; set; }
    }


    public class ApiResponse
    {
        public ApiResponse(ModelStateDictionary modelState = null)
        {
            if (modelState != null)
            {
                ModelState = new Dictionary<string, IEnumerable<string>>();
                foreach (var pair in modelState)
                {
                    ModelState.Add(pair.Key, pair.Value.Errors.Select(x => x.ErrorMessage));
                }
            }
        }

        public bool Status { get; set; } = false;
        public string ErrorMessage { get; set; }
        public Dictionary<string, IEnumerable<string>> ModelState { get; set; }        
    }
}
