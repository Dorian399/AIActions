using AIActions.AI.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AIActions.Settings
{
    internal static class Updater
    {
        private class GithubResponse
        {
            public string? tag_name { get; set; }
            public string? html_url { get; set; }
        }
        private static string repoLatestReleaseUrl = "https://api.github.com/repos/Dorian399/AIActions/releases/latest";
        public static async Task<(bool shouldUpdate, string status)> GetUpdateStatusAsync()
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("GET"), repoLatestReleaseUrl);

                request.Headers.Add("User-Agent", "AIActions Updater");
                request.Headers.Add("Accept", "application/vnd.github+json");

                HttpResponseMessage response = await httpClient.SendAsync(request);
                string responseString = await response.Content.ReadAsStringAsync();
                GithubResponse? responseParsed = JsonSerializer.Deserialize<GithubResponse>(responseString);

                if(responseParsed != null && 
                    responseParsed.tag_name != null &&
                    responseParsed.html_url != null
                    )
                {
                    string cleanVersion = responseParsed.tag_name.Replace("v", "");
                    string currentVersion = Application.ProductVersion.Split("+")[0];

                    if(!string.Equals(currentVersion,cleanVersion))
                        return (true, responseParsed.html_url);
                    else
                        return (false, "No updates found.");
                }
                else
                {
                    throw new Exception("Failed to retrieve tag_name");
                }
            }
            catch (Exception ex)
            {
                return (false, "Failed to retrieve update info.");
            }
        }
    }
}
