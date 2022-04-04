using System.Collections.Generic;
using CodeBase.Card;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Logic
{
	public class HandCardPlacer : CardPlacerBase
	{
		[SerializeField] private Transform handCenter;
		[SerializeField] private float maxArcAngle = 45f;
		[SerializeField] private float arcStep = 5f;
		[SerializeField] private float arcRadius = 1f;

		private List<CardBase> _cards = new List<CardBase>();

		protected override void OnEnable()
		{
			base.OnEnable();
			CardHolder.CardAdded += AddCard;
			CardHolder.CardRemoved += RemoveCard;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			CardHolder.CardAdded -= AddCard;
			CardHolder.CardRemoved -= RemoveCard;
		}

		private void AddCard(CardBase card)
		{
			card.OnDragEnded += UpdateCardPlace;
			_cards.Add(card);
		}

		private void RemoveCard(CardBase card)
		{
			card.OnDragEnded -= UpdateCardPlace;
			_cards.Remove(card);
		}

		[Sirenix.OdinInspector.Button]
		protected override void UpdateCardsPlaces()
		{
			var cardsCount = _cards.Count;

			var arcAngle = CalculateArcAngle(cardsCount);
			var angleStep = CalculateAngleStep(arcAngle, cardsCount);
			var startAngle = CalculateStartAngle(cardsCount, arcAngle);

			for (int i = 0; i < cardsCount; i++)
			{
				var card = _cards[i];
				UpdateCardPlace(card, i, startAngle, angleStep);
			}
		}

		private void UpdateCardPlace(CardBase card, int index, float startAngle, float angleStep)
		{
			var angleTwist = startAngle - angleStep * index;

			var cardPosition = GetCardArcPosition(angleTwist);
			var cardRotation = GetTwistCardRotation(card.transform, angleTwist);

			CardMover.MoveWithRotation(card, cardPosition, cardRotation);
		}

		private void UpdateCardPlace(CardBase card)
		{
			var index = _cards.IndexOf(card);
			var cardsCount = _cards.Count;

			var arcAngle = CalculateArcAngle(cardsCount);
			var angleStep = CalculateAngleStep(arcAngle, cardsCount);
			var startAngle = CalculateStartAngle(cardsCount, arcAngle);

			UpdateCardPlace(card, index, startAngle, angleStep);
		}

		private float CalculateArcAngle(int cardsCount) =>
			cardsCount * arcStep > maxArcAngle ? maxArcAngle : cardsCount * arcStep;

		private float CalculateAngleStep(float arcAngle, int cardsCount) =>
			arcAngle / (cardsCount > 1 ? cardsCount - 1 : 1);

		private float CalculateStartAngle(int cardsCount, float arcAngle) =>
			cardsCount == 1 ? 0 : arcAngle / 2;

		private Vector3 GetCardArcPosition(float angleTwist)
		{
			var handPosition = handCenter.transform.position;
			var point = TrigonometryUtils.GetPointOnCircle(angleTwist, arcRadius);
			return new Vector3
			{
				x = handPosition.x + -point.x,
				y = handPosition.y,
				z = handPosition.z + point.y - arcRadius
			};
		}

		private Vector3 GetTwistCardRotation(Transform targetTransform, float angleTwist)
		{
			var cardRotation = targetTransform.eulerAngles;
			cardRotation.y = -angleTwist;
			return cardRotation;
		}
	}
}