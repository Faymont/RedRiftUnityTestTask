using CodeBase.Card.Data;
using UnityEngine;

namespace CodeBase.Extensions
{
	public static class CardMics
	{
		public static CardData GetRandomCardData(int from = 1, int to = 10)
		{
			return new CardData
			{
				Health = Random.Range(from, to),
				Mana = Random.Range(from, to),
				Power = Random.Range(from, to),
				URL = "https://picsum.photos/200/300"
			};
		}
	}
}