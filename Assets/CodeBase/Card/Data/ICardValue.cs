using System;

namespace CodeBase.Card.Data
{
	public interface ICardValue
	{
		int Value { get; set; }
		event Action<int> ValueChanged;
		void LoadState(CardData cardData);
	}
}