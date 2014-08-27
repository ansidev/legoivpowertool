﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoIV_Power_Tool
{
    class GitHubRelease
    {
        public string url { get; set; }
        public string asset_url { get; set; }
        public string upload_url { get; set; }
        public string html_url { get; set; }
        public int id { get; set; }
        public string tag_name { get; set; } 
        public string target_commitish { get; set; }
        public string name { get; set; }
        public bool draft { get; set; }
        public GitHubAuthor author { get; set; }
        public bool prerelease { get; set; }
        public DateTime created_at { get; set; }
        public DateTime published_at { get; set; }
        public List<GitHubReleaseAsset> asset { get; set; }
        public string tarbal_url { get; set; }
        public string zipball_url { get; set; }
        public string body { get; set; }
    }
}
