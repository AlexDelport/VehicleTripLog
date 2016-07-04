using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;

namespace VeLogDataApp.Models
{
    public class VeLogDataClient
    {
        private string Base_Url = "http://velogtest.azurewebsites.net/api/VeLogData";
        public bool Create(VeLogData LogDataRec)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_Url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("VeLogData", LogDataRec).Result;
               
                return response.IsSuccessStatusCode;
                
            }
            catch
            {
                return false;
            }
        }
    }
}