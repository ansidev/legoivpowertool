using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoIV_Power_Tool
{
    class GitHubReleaseAsset
    {
        [JsonProperty("url")]
        public string url { get; set; }
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("label")]
        public string label { get; set; }
        [JsonProperty("uploader")]
        public GitHubAuthor uploader { get; set; }
        [JsonProperty("content_type")]
        public string content_type { get; set; }
        [JsonProperty("state")]
        public string state { get; set; }
        [JsonProperty("size")]
        public int size { get; set; }
        [JsonProperty("download_count")]
        public int download_count { get; set; }
        [JsonProperty("created_at")]
        public DateTime created_at { get; set; }
        [JsonProperty("updated_at")]
        public DateTime updated_at { get; set; }
        [JsonProperty("browser_download_url")]
        public string browser_download_url { get; set; }

    }
}
