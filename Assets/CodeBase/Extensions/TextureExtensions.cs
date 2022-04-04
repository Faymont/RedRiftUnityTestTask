using UnityEngine;

namespace CodeBase.Extensions
{
	public static class TextureExtensions
	{
		public static Sprite ConvertToSprite(this Texture2D texture) =>
			Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
	}
}