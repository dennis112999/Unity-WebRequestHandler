using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Cysharp.Threading.Tasks;
using Tools;

namespace WebRequest.Sample
{
    /// <summary>
    /// This class handles the downloading and playing of a video from a given URI using a VideoPlayer.
    /// </summary>
    public class VideoWebRequester : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private Button _downloadButton;

        [Header("Download Settings")]
        [SerializeField] private string _videoUri;

        #region MonoBehaviour

        private void Awake()
        {
            Initialize();
        }

        #endregion MonoBehaviour

        private void Initialize()
        {
            _downloadButton.onClick.AddListener(OnDownloadButtonClick);
        }

        #region Button

        private void OnDownloadButtonClick()
        {
            var cancellationToken = this.GetCancellationTokenOnDestroy();
            DownloadAndPlayVideo(cancellationToken).Forget();
        }

        #endregion Button

        #region Download

        /// <summary>
        /// Downloads a video from the specified URI and plays it using the VideoPlayer.
        /// </summary>
        /// <param name="cancellationToken">Token to handle cancellation on object destruction.</param>
        /// <returns></returns>
        private async UniTaskVoid DownloadAndPlayVideo(CancellationToken cancellationToken)
        {
            bool success = await WebRequestHandler.GetVideo(_videoUri, _videoPlayer, cancellationToken);

            if (success)
            {
#if UNITY_EDITOR
                Debug.Log("Video downloaded and playing successfully.");
#endif
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("Failed to load video from URI: " + _videoUri);
#endif
            }
        }

        #endregion Download
    }
}
