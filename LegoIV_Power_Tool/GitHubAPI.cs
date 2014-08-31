using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoIV_Power_Tool
{
    class GitHubAPI
    {
        public GitHubReleaseAsset ReleaseAsset(List<GitHubRelease> releases, string _arch )
        {
            GitHubReleaseAsset _asset = new GitHubReleaseAsset();
            foreach (GitHubReleaseAsset asset in releases[0].assets)
            {
                if(asset.name.Contains(_arch))
                {
                    _asset = asset;
                    break;
                }
            }
            return _asset;
        }
    }
}
