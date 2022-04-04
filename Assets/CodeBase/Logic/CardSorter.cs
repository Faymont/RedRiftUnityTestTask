using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.Logic
{
	[RequireComponent(typeof(CardHolder))]
	public class CardSorter : MonoBehaviour
	{
		public float YOffsetStep = 0.1f;

		private ICardHolder _cardHolder;

		protected virtual void Awake()
		{
			_cardHolder = GetComponent<ICardHolder>();
		}

		protected virtual void OnEnable()
		{
			_cardHolder.CardHolderUpdated += UpdateSortOrder;
		}

		protected virtual void OnDisable()
		{
			_cardHolder.CardHolderUpdated -= UpdateSortOrder;
		}

		private void UpdateSortOrder()
		{
			var cards = _cardHolder.Cards;

			for (var i = 0; i < cards.Count; i++)
			{
				var card = cards[i];

				card.CardVisuals.ChangeSortOrder(i);

				var cardTransform = card.transform;
				var cardPosition = cardTransform.position;
				cardTransform.position = cardPosition.AddY(YOffsetStep);
			}
		}
	}
}