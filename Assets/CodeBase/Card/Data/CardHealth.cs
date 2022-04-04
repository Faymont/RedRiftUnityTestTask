using System;
using UnityEngine;

namespace CodeBase.Card.Data
{
	public class CardHealth : MonoBehaviour, ICardValue
	{
		private CardData _data;

		public void LoadState(CardData cardData)
		{
			_data = cardData;

			ValueChanged?.Invoke(Value);
		}

		public int Value
		{
			get => _data.Health;
			set
			{
				if (_data.Health != value)
				{
					_data.Health = value;

					ValueChanged?.Invoke(Value);
				}
			}
		}

		public event Action<int> ValueChanged;
	}
}