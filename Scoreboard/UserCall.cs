using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Scoreboard
{
    public class UserCall
    {
        private static string baseUrl = "http://77.175.219.85:9090";

        public async Task<User> getUser()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/scoreboard/api/users/test");
                User u = null;
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("RESPONSE: " + jsonResponse);
                    u = JsonConvert.DeserializeObject<User>(jsonResponse);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Something went wrong with the API call. Status code: " + response.StatusCode);
                    throw new WebException("Could not reach the server. Status code: " + response.StatusCode);
                }
                return u;
            }
        }
    }
}
