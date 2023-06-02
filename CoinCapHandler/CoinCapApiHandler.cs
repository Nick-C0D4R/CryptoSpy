using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoinCapHandler
{
    public class CoinCapApiHandler
    {
        private HttpClient _client = new HttpClient();
        public string Url => "https://api.coincap.io/v2";

        public async Task<JObject> GetJson(string url)
        {
            HttpResponseMessage message = await _client.GetAsync(url);
            if (message.IsSuccessStatusCode)
            {
                string response = JsonConvert.SerializeObject(message.Content);
                return JObject.Parse(response);
            }

            if ((int)message.StatusCode >= 400 && (int)message.StatusCode <= 417)
                throw new ArgumentException("Invalid URL");
            
            throw new CannotUnloadAppDomainException("Server Error");
        }
    }
}
