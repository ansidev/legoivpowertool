using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoIV_Power_Tool
{
    class GitHubRelease
    {
        [JsonProperty("url")]
        public string url { get; set; }
        [JsonProperty("assets_url")]
        public string assets_url { get; set; }
        [JsonProperty("upload_url")]
        public string upload_url { get; set; }
        [JsonProperty("html_url")]
        public string html_url { get; set; }
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("tag_name")]
        public string tag_name { get; set; }
        [JsonProperty("target_commitish")]
        public string target_commitish { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("draft")]
        public bool draft { get; set; }
        [JsonProperty("author")]
        public GitHubAuthor author { get; set; }
        [JsonProperty("prerelease")]
        public bool prerelease { get; set; }
        [JsonProperty("created_at")]
        public DateTime created_at { get; set; }
        [JsonProperty("published_at")]
        public DateTime published_at { get; set; }
        [JsonProperty("assets")]
        public List<GitHubReleaseAsset> assets { get; set; }
        [JsonProperty("tarball_url")]
        public string tarball_url { get; set; }
        [JsonProperty("zipball_url")]
        public string zipball_url { get; set; }
        [JsonProperty("body")]
        public string body { get; set; }

        //Method
        public GitHubRelease()
        {
            //author = new GitHubAuthor();
            assets = new List<GitHubReleaseAsset>();
        }
        public string DisplayReleaseInfo()
        {
            string result = null;
            result = "Version: " + name.Split(' ')[3].Substring(1) + Environment.NewLine +
                "Release Time: " + published_at + Environment.NewLine +
                "Release ID: " + id.ToString() + Environment.NewLine;
            //GitHubReleaseAsset _asset = GhRelease.asset[1];
            //result += "Download URL: " + GhRelease.asset[1].browser_download_url + Environment.NewLine;
            return result;
        }
        public int GetReleaseVersion()
        {
            return Int16.Parse(tag_name.Split('.')[0] + tag_name.Split('.')[1]);
        }
    }
}
