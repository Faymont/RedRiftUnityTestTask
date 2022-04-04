using System;
using DG.Tweening;
using TMPro;

namespace CodeBase.Extensions
{
	public static class DOTweenExtenstions
	{
		public static Tweener DOTextCounter(this TMP_Text text, int to, float duration,
			Func<int, string> convertor)
		{
			var initValue = int.Parse(text.text);
			return DOTween.To(
				() => initValue,
				it => text.text = convertor(it),
				to,
				duration
			);
		}

		public static Tweener DOTextIntCounter(this TMP_Text text, int to, float duration)
		{
			return DOTextCounter(text, to, duration, it => it.ToString());
		}
	}
}