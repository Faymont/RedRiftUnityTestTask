using System;
using UnityEngine;

namespace CodeBase.Card.Data
{
	[RequireComponent(typeof(CardVisuals))]
	public class CardPower : MonoBehaviour, ICardValue
	{
		private CardData _data;

		public void LoadState(CardData cardData)
		{
			_data = cardData;

			ValueChanged?.Invoke(Value);
		}

		public int Value
		{
			get => _data.Power;
			set
			{
				if (_data.Power != value)
				{
					_data.Power = value;

					ValueChanged?.Invoke(Value);
				}
			}
		}

		public event Action<int> ValueChanged;
	}
}