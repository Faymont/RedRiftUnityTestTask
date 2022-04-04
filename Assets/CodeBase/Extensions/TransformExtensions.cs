using UnityEngine;

namespace CodeBase.Extensions
{
	public static class TransformExtensions
	{
		public static Vector3 AddY(this Vector3 v, float y) => new Vector3(v.x, v.y + y, v.z);
	}
}