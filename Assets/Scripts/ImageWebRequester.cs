using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Tools;

namespace WebRequest.Sample
{
    /// <summary>
    /// This class demonstrates how to download and display an image from the web in RawImage.
    /// </summary>
    public class ImageWebRequester : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private RawImage _targetImage;
        [SerializeField] Button _downloadButton;

        [Header("Download Link")]
        [SerializeField] private string _uri;

        #region MonoBehaviour

        void Awake()
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
            var token = this.GetCancellationTokenOnDestroy();
            GetTexture(token).Forget();
        }

        #endregion Button

        /// <summary>
        /// Downloads an image from the specified URI and applies it to the RawImage.
        /// </summary>
        /// <param name="cancellationToken">Token to handle cancellation on object destruction.</param>
        /// <returns></returns>
        private async UniTaskVoid GetTexture(CancellationToken cancellationToken)
        {
            // Request the texture from the WebRequestHandler
            Texture texture = await WebRequestHandler.GetTexture(_uri, cancellationToken);

            if (texture != null)
            {
                _targetImage.texture = texture;
            }
            else
            {
                Debug.LogError("Failed to load texture from URI: " + _uri);
            }
        }
    }

}