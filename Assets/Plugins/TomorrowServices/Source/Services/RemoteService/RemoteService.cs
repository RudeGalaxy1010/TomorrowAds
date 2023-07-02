using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Tomorrow.Services
{
    public class RemoteService
    {
        public async Task<TResult> GetHttp<TResult>(string url, IDeserializer deserializer, string contentType = "")
        {
            try
            {
                using var www = UnityWebRequest.Get(url);

                if (string.IsNullOrEmpty(contentType) == false)
                {
                    www.SetRequestHeader("Content-Type", contentType);
                }

                var operation = www.SendWebRequest();

                while (operation.isDone == false)
                {
                    await Task.Yield();
                }

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Failed: {www.error}");
                }

                var result = deserializer.Deserialize<TResult>(www.downloadHandler.text);

                return result;
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameof(GetHttp)} failed: {ex.Message}");
                return default;
            }
        }

        public async Task<Texture> GetTexture(string url, string contentType = "")
        {
            Texture result = null;

            try
            {
                using (var www = UnityWebRequestTexture.GetTexture(url))
                {
                    if (string.IsNullOrEmpty(contentType) == false)
                    {
                        www.SetRequestHeader("Content-Type", contentType);
                    }

                    var operation = www.SendWebRequest();

                    while (operation.isDone == false)
                    {
                        await Task.Yield();
                    }

                    if (www.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError($"Failed: {www.error}");
                    }
                    else
                    {
                        result = DownloadHandlerTexture.GetContent(www);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameof(GetTexture)} failed: {ex.Message}");
                return default;
            }
        }
    }
}
