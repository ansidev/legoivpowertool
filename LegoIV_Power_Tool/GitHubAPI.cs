using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoIV_Power_Tool
{
    class GitHubAPI
    {
        public string DownloadURL(List<GitHubReleaseAsset> assets)
        {
            string[] download_link = new string[2];
            int i = 0;
            foreach (GitHubReleaseAsset asset in assets)
            {
                download_link[i] = asset.browser_download_url;
                i++;
            }
            return String.Join(Environment.NewLine, download_link);
        }
    }
}
