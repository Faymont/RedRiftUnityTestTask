using System;
using UnityEngine;

namespace CodeBase.Card.Data
{
	[RequireComponent(typeof(CardVisuals))]
	public class CardMana : MonoBehaviour, ICardValue
	{
		private CardData _data;

		public void LoadState(CardData cardData)
		{
			_data = cardData;

			ValueChanged?.Invoke(Value);
		}

		public int Value
		{
			get => _data.Mana;
			set
			{
				if (_data.Mana != value)
				{
					_data.Mana = value;

					ValueChanged?.Invoke(Value);
				}
			}
		}

		public event Action<int> ValueChanged;
	}
}