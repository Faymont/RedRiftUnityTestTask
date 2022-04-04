using CodeBase.Card;
using CodeBase.Card.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.GamePlay
{
	public class CardEngine : MonoBehaviour
	{
		[SerializeField] private CardHolder playerHand;
		[SerializeField] private CardHolder table;
		[SerializeField] private Transform cardPile;
		[SerializeField] private Transform trashPile;

		private CardFactory _cardFactory;
		private CardMover _cardMover;
		private Sequence _shuffleSequence;

		[Inject]
		public void Construct(CardFactory cardFactory, CardMover cardMover)
		{
			_cardFactory = cardFactory;
			_cardMover = cardMover;
		}

		private void Start()
		{
			for (int i = 0; i < Random.Range(4, 7); i++)
			{
				GiveCardToPlayer();
			}
		}

		[Sirenix.OdinInspector.Button]
		public void GiveCardToPlayer()
		{
			var card = SpawnCard(cardPile.transform.position);
			playerHand.AddCard(card);
		}

		public CardBase SpawnCard(Vector3 position)
		{
			var card = _cardFactory.Create();

			card.transform.position = position;

			return card;
		}

		public void PlayCard(CardBase card)
		{
			playerHand.RemoveCard(card);
			table.AddCard(card);
		}

		public void DiscardCard(CardBase card)
		{
			card.CurrentPile?.RemoveCard(card);
			_cardMover.MoveWithRotation(card, trashPile.position, trashPile.eulerAngles);
			Destroy(card.gameObject, 2f);
		}

		[Sirenix.OdinInspector.Button]
		public void ShuffleAddCardsProperty()
		{
			_shuffleSequence?.Complete();
			_shuffleSequence = DOTween.Sequence();

			var timeStep = 0.2f;
			var insertTime = 0f;

			foreach (var card in playerHand.Cards.ToArray())
			{
				_shuffleSequence.InsertCallback(insertTime, () => ShuffleCardProperty(card));
				insertTime += timeStep;
			}

			foreach (var card in table.Cards.ToArray())
			{
				_shuffleSequence.InsertCallback(insertTime, () => ShuffleCardProperty(card));
				insertTime += timeStep;
			}
		}

		private void ShuffleCardProperty(CardBase card)
		{
			var cardProperty = GetRandomCardProperty(card);
			cardProperty.Value = Random.Range(-2, 10);
		}


		private ICardValue GetRandomCardProperty(CardBase card)
		{
			var count = card.CardValues.Count;
			return card.CardValues[Random.Range(0, count)];
		}
	}
}