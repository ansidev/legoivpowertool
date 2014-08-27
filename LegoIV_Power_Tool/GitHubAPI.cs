using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoIV_Power_Tool
{
    class GitHubAPI
    {
        public string DisplayReleaseInfo(GitHubRelease GhRelease)
        {
            string result = null;
            result = "Version: " + GhRelease.tag_name + Environment.NewLine +
                "Release Time: " + GhRelease.published_at + Environment.NewLine;
                result += "Download URL: " + GhRelease.asset[0].browser_download_url + Environment.NewLine;
            return result;
        }
    }
}
