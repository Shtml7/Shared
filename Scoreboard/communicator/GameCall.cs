﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Scoreboard.domain;
using System.Collections.Generic;
using System.Text;

namespace Scoreboard.communicator
{
    public static class GameCall
    {
        private static string baseUrl = "http://77.175.219.85:9090/";

        public static async Task<List<Game>> GetAllGames()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("scoreboard/api/games");
                List<Game> games = null;
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("RESPONSE: " + jsonResponse);
                    games = JsonConvert.DeserializeObject<List<Game>>(jsonResponse);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Something went wrong with the API call. Status code: " + response.StatusCode);
                    throw new WebException("Something went wrong with the API call. Status code: " + response.StatusCode);
                }
                return games;
            }
        }

        public static async Task<Game> createGame(Game game)
        {
            using (var client = new HttpClient())
            {
				client.BaseAddress = new Uri(baseUrl);

                try
                {
                    string gameJson = JsonConvert.SerializeObject(game);
                    System.Diagnostics.Debug.WriteLine("Going to post now: " + gameJson);
					var response = await client.PostAsync("scoreboard/api/games", new StringContent(gameJson, Encoding.UTF8, "application/json"));
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonstring = await response.Content.ReadAsStringAsync();
                        System.Diagnostics.Debug.WriteLine("UPLOAD RESULT: " + jsonstring);
						return JsonConvert.DeserializeObject<Game>(jsonstring);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("UPLOAD EXCEPTION: " + ex);
                }
				return null;
            }

        }

        public static async Task<Game> updateGame(Game game)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    string gameJson = JsonConvert.SerializeObject(game);
                    System.Diagnostics.Debug.WriteLine("Going to put now");
                    var response = await client.PutAsync("scoreboard/api/games", new StringContent(gameJson, Encoding.UTF8, "application/json"));
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonstring = await response.Content.ReadAsStringAsync();
                        System.Diagnostics.Debug.WriteLine("UPLOAD RESULT: " + jsonstring);
                        return JsonConvert.DeserializeObject<Game>(jsonstring);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("UPLOAD EXCEPTION: " + ex);
                }
                return null;
            }

        }
    }
}
