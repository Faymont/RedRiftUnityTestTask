using System;
using System.Collections.Generic;
using CodeBase.Card;

namespace CodeBase.Logic
{
	public interface ICardHolder
	{
		List<CardBase> Cards { get; }
		event Action CardHolderUpdated;
		event Action<CardBase> CardAdded;
		event Action<CardBase> CardRemoved;
		void AddCard(CardBase card);
		void RemoveCard(CardBase card);
	}
}