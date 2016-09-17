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

		public static async Task<List<User>> getUsers()
		{
			using (var client = new HttpClient())
			{
				try
				{
					client.BaseAddress = new Uri(baseUrl);
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					HttpResponseMessage response = await client.GetAsync("scoreboard/api/users");
					List<User> user = null;
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

		public static async Task<int> createUser(byte[] data, String ext, User user)
		{
			using (var client = new HttpClient())
			{
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
					multi.Add(new StringContent(user.username), "name");
					multi.Add(new StringContent(ext), "extension");

					System.Diagnostics.Debug.WriteLine("Going to post now");
					var response = await client.PostAsync("scoreboard/api/users/upload", multi);
					response.EnsureSuccessStatusCode();

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

		public static async Task<User> getUserWithid(int id)
		{
			using (var client = new HttpClient())
			{
				try
				{
					client.BaseAddress = new Uri(baseUrl);
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					HttpResponseMessage response = await client.GetAsync("scoreboard/api/users/" + id);
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
