using System.Net;
using tibexr.Util;
using System.IO.Compression;

namespace tibexr.Network
{
    public class ClientDownloadMirror
    {
        static string MIRROR_URL = "https://github.com/thebma/tibiaexr/blob/master/mirror/{0}.zip?raw=true";

        public static bool CheckMirror(string shortcode)
        {
            try
            {
                string url = string.Format(MIRROR_URL, shortcode);
                HttpWebRequest webReq = WebRequest.Create(url) as HttpWebRequest;
                webReq.Method = "HEAD";

                HttpWebResponse webResp = webReq.GetResponse() as HttpWebResponse;
                HttpStatusCode code = webResp.StatusCode;

                webResp.Close();

                if (code == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool FetchDistro(string shortCode)
        {
            CommonFiles.PrepareDistroFolder();
            CommonFiles.PrepareDistro(shortCode);

            if (!CheckMirror(shortCode))
            {
                PrettyPrint.PPStep("FetchCommand");
                PrettyPrint.PPFormat($"The distro for {shortCode} does not exist.");
                return false;
            }

            GetDistroFiles(shortCode);
            return true;
        }

        private static void GetDistroFiles(string shortCode)
        {
            //TODO: Proper error handling!
            using (var client = new WebClient())
            {
                client.DownloadFile(
                    string.Format(MIRROR_URL, shortCode),
                    $"distro/{shortCode}/{shortCode}.zip"
                );
            }

            using (ZipArchive zipFile = ZipFile.OpenRead($"distro/{shortCode}/{shortCode}.zip"))
            {
                zipFile.ExtractToDirectory($"distro/{shortCode}", true);
            }
       }
    }
}
