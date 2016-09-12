using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Splat;
using System.Collections.Generic;
using Scoreboard.domain;
using Android.Graphics;
using Java.IO;
using Java.Net;

namespace Scoreboard
{
    public static class UserCall
    {
        private static string baseUrl = "http://77.175.219.85:9090/";

        public static async Task<User> getUser()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = await client.GetAsync("scoreboard/api/users/test");
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

		public static async void UploadImage(byte[] data, String ext, User user)
		{
			using (var client = new HttpClient()) {
				client.BaseAddress = new Uri(baseUrl);

				try
				{
					var multi = new MultipartFormDataContent();
					var imageContent = new StreamContent(new MemoryStream(data));

					if (ext.Equals("png"))
					{
						imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
					}
					else if (ext.Equals("jpeg") || ext.Equals("jpg"))
					{
						imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
					}

					multi.Add(imageContent, "file");
					multi.Add(new StringContent(user.name), "name");
					multi.Add(new StringContent(ext), "extension");

					System.Diagnostics.Debug.WriteLine("Going to post now");
					var response = await client.PostAsync("scoreboard/api/users/upload", multi);
					response.EnsureSuccessStatusCode();

					if (response.IsSuccessStatusCode)
					{
						var jsonstring = await response.Content.ReadAsStringAsync();
						System.Diagnostics.Debug.WriteLine("UPLOAD RESULT: " + jsonstring);
					}

				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine("UPLOAD EXCEPTION: " + ex);

				}
			}
			
		}

        public static byte[] ReadFully(Stream input)
		{
			byte[] buffer = new byte[16 * 1024];
			using (MemoryStream ms = new MemoryStream())
			{
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}       
        
    }
}
