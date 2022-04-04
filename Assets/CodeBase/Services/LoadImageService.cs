using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Services
{
	public class LoadImageService
	{
		public async void DownloadImage(string url, Action<Texture2D> onLoad)
		{
			await GetTextureAsync(url, onLoad);
		}

		private async UniTask<Texture2D> GetTextureAsync(string url, Action<Texture2D> onLoad)
		{
			var request = UnityWebRequestTexture.GetTexture(url);
			var op = await request.SendWebRequest();

			if (request.isNetworkError || request.isHttpError)
				Debug.Log(request.error);
			else
			{
				var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
				onLoad?.Invoke(texture);
			}

			return ((DownloadHandlerTexture)op.downloadHandler).texture;
		}
	}
}