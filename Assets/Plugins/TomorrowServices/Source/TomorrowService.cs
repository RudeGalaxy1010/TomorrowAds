using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Tomorrow.Services
{
    public class TomorrowService : MonoBehaviour
    {
        [SerializeField] private RawImage _rawImage;

        private ServiceStatus _status;

        private void Start()
        {
            _status = ServiceStatus.Running;
            Setup();
        }

        private void OnDestroy()
        {
            if (_rawImage.texture != null)
            {
                Destroy(_rawImage.texture);
            }
        }

        private async void Setup()
        {
            RemoteService remoteService = new RemoteService();

            try
            {
                Task<Reply> replyTask = DownloadData(remoteService);
                Task<Texture> textureTask = DownloadTexture(remoteService);

                await Task.WhenAll(replyTask, textureTask);

                Debug.Log(replyTask.Result);
                _rawImage.texture = textureTask.Result;
            }
            catch (Exception exception)
            {
                _status = ServiceStatus.Failed;
                Debug.LogError($"Setup failed with {exception.Message}");
            }

            _status = ServiceStatus.Ready;
        }

        private async Task<Reply> DownloadData(RemoteService remoteService)
        {
            IDeserializer deserializer = new JsonDeserializer();
            return await remoteService.GetHttp<Reply>("http://192.168.0.106:8070/reply.json", deserializer);
        }

        private async Task<Texture> DownloadTexture(RemoteService remoteService)
        {
            return await remoteService.GetTexture("http://192.168.0.106:8070/Avatars.jpg");
        }
    }

    public class Reply
    {
        public int A;

        public override string ToString()
        {
            return $"A = {A}";
        }
    }
}
