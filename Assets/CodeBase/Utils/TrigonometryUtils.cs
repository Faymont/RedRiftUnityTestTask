using UnityEngine;

namespace CodeBase.Utils
{
	public static class TrigonometryUtils
	{
		public static Vector2 GetPointOnCircle(float angle, float radius)
		{
			return new Vector2
			{
				x = radius * Mathf.Sin(angle * Mathf.Deg2Rad),
				y = radius * Mathf.Cos(angle * Mathf.Deg2Rad)
			};
		}
	}
}