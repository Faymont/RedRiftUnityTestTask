using UnityEngine;
using Zenject;

namespace CodeBase.Logic
{
	[RequireComponent(typeof(CardHolder))]
	public abstract class CardPlacerBase : MonoBehaviour
	{
		protected CardMover CardMover;
		protected ICardHolder CardHolder;

		[Inject]
		public void Construct(CardMover cardMover)
		{
			CardMover = cardMover;
		}

		protected virtual void Awake()
		{
			CardHolder = GetComponent<ICardHolder>();
		}

		protected virtual void OnEnable()
		{
			CardHolder.CardHolderUpdated += UpdateCardsPlaces;
		}

		protected virtual void OnDisable()
		{
			CardHolder.CardHolderUpdated -= UpdateCardsPlaces;
		}

		protected abstract void UpdateCardsPlaces();
	}
}