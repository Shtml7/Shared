using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Scoreboard
{
	public static class UserCall
	{
		private static string baseUrl = "http://77.175.219.85:9090/";

        /**
         * Get all the users from the server
         * Returns a list with users
         */
        public static async Task<List<User>> getUsers()
		{
			using (var client = new HttpClient())
			{
				try
				{
                    //Set the headers for the call
                    client.BaseAddress = new Uri(baseUrl);
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Create a get call
                    HttpResponseMessage response = await client.GetAsync("scoreboard/api/users");
					List<User> user = null;

                    //Get a response from the server
                    response.EnsureSuccessStatusCode();
					string jsonResponse = await response.Content.ReadAsStringAsync();
					System.Diagnostics.Debug.WriteLine("RESPONSE: " + jsonResponse);
					user = JsonConvert.DeserializeObject<List<User>>(jsonResponse);
					return user;
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine("Something went wrong with the API call. Status code: " + ex.Message);
				}
				return null;
			}
		}

        /**
         * Create a user with a image and username
         */
		public static async Task<int> createUser(byte[] data, String ext, User user)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(baseUrl);

				try
				{
                    //Create a multipart for the image upload 
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

                    //Add some headers for the multipart
					multi.Add(imageContent, "file");
					multi.Add(new StringContent(user.username), "name");
					multi.Add(new StringContent(ext), "extension");

                    //Create a post call
					System.Diagnostics.Debug.WriteLine("Going to post now");
					var response = await client.PostAsync("scoreboard/api/users/upload", multi);
					response.EnsureSuccessStatusCode();

                    //Get response from the server
					if (response.IsSuccessStatusCode)
					{
						var jsonstring = await response.Content.ReadAsStringAsync();
						System.Diagnostics.Debug.WriteLine("UPLOAD RESULT: " + jsonstring);
						return int.Parse(jsonstring);
					}

				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine("UPLOAD EXCEPTION: " + ex);

				}
				return -1;
			}

		}

        /**
         * Get a user by a id
         */
        public static async Task<User> getUserWithid(int id)
		{
			using (var client = new HttpClient())
			{
				try
				{
                    //Set the headers for the call
                    client.BaseAddress = new Uri(baseUrl);
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Create get call
                    HttpResponseMessage response = await client.GetAsync("scoreboard/api/users/" + id);

                    //Response from the server
                    if (response.IsSuccessStatusCode)
					{
						string jsonResponse = await response.Content.ReadAsStringAsync();
						System.Diagnostics.Debug.WriteLine("RESPONSE: " + jsonResponse);
						return JsonConvert.DeserializeObject<User>(jsonResponse);
					}
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine("Something went wrong with the API call. Exception: " + ex.Message);
				}
			}
			return null;
		}
	}
}
