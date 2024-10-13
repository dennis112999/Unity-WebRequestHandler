using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

namespace Tools
{
    public static class WebRequestHandler
    {
        /// <summary>
        /// Fetches a texture from the given URI.
        /// </summary>
        /// <param name="uri">The URI of the texture.</param>
        /// <param name="cancellationToken">The cancellation token for task cancellation.</param>
        /// <returns>The downloaded texture, or null if the request failed.</returns>
        public static async UniTask<Texture> GetTexture(string uri, CancellationToken cancellationToken)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(uri);
            await www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
#if UNITY_EDITOR
                Debug.LogError(www.error);
                Debug.LogError("Failed to load texture from URI: " + uri);
#endif
                return null;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Image downloaded successfully.");
#endif
                return ((DownloadHandlerTexture)www.downloadHandler).texture;
            }
        }

        /// <summary>
        /// Fetches and plays a video from the given URI using the provided VideoPlayer.
        /// </summary>
        /// <param name="uri">The URI of the video.</param>
        /// <param name="videoPlayer">The VideoPlayer component to play the video.</param>
        /// <param name="cancellationToken">The cancellation token for task cancellation.</param>
        /// <returns>True if the video was successfully loaded and played, otherwise false.</returns>
        public static async UniTask<bool> GetVideo(string uri, VideoPlayer videoPlayer, CancellationToken cancellationToken)
        {
            UnityWebRequest www = UnityWebRequest.Get(uri);
            await www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
#if UNITY_EDITOR
                Debug.LogError(www.error);
                Debug.LogError("Failed to load video from URI: " + uri);
#endif
                return false;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Video downloaded successfully.");
#endif
                // Set the URL for the video player and start playing the video
                videoPlayer.url = uri;
                videoPlayer.Prepare();

                // Wait until the video is prepared
                await UniTask.WaitUntil(() => videoPlayer.isPrepared, cancellationToken: cancellationToken);

                videoPlayer.Play();
                return true;
            }
        }
    }

}