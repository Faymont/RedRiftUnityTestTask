using System;
using System.Collections.Generic;
using CodeBase.Card;
using UnityEngine;

namespace CodeBase.Logic
{
	public class CardHolder : MonoBehaviour, ICardHolder
	{
		public List<CardBase> Cards { get; } = new List<CardBase>();

		public event Action CardHolderUpdated;
		public event Action<CardBase> CardAdded;
		public event Action<CardBase> CardRemoved;

		public void AddCard(CardBase card)
		{
			card.CurrentPile = this;
			card.transform.SetParent(transform);

			Cards.Add(card);

			CardAdded?.Invoke(card);
			CardHolderUpdated?.Invoke();
		}

		public void RemoveCard(CardBase card)
		{
			card.CurrentPile = this;
			Cards.Remove(card);

			CardRemoved?.Invoke(card);
			CardHolderUpdated?.Invoke();
		}
	}
}