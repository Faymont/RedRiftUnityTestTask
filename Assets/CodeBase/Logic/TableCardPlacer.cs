using UnityEngine;

namespace CodeBase.Logic
{
	public class TableCardPlacer : CardPlacerBase
	{
		[SerializeField] private Transform tableCenter;
		[SerializeField] private float widthStep;
		[SerializeField] private float maxWidth;

		[Sirenix.OdinInspector.Button]
		protected override void UpdateCardsPlaces()
		{
			var cards = CardHolder.Cards;
			var cardsCount = cards.Count;
			Vector3 tableCenterPosition = tableCenter.transform.position;

			var tableWidth = CalculateTableWidth(cardsCount);
			var startPoint = CalculateStartPoint(cardsCount, tableCenterPosition, tableWidth);
			var cardWidthStep = CalculateCardWidthStep(tableWidth, cardsCount);

			for (int i = 0; i < cardsCount; i++)
			{
				var card = cards[i];

				var verticalShift = startPoint + cardWidthStep * i;
				var cardPosition = GetCardPosition(verticalShift, tableCenterPosition);

				CardMover.MoveCard(card, cardPosition, Quaternion.identity);
			}
		}

		private float CalculateTableWidth(int cardsCount) => 
			cardsCount * widthStep > maxWidth ? maxWidth : cardsCount * widthStep;

		private float CalculateStartPoint(int cardsCount, Vector3 tableCenterPosition, float tableWidth)
		{
			var halfWidth = tableWidth / 2;
			return cardsCount == 1 ? tableCenterPosition.x : tableCenterPosition.x - halfWidth;
		}

		private Vector3 GetCardPosition(float verticalShift, Vector3 center) =>
			new Vector3(center.x + verticalShift, center.y, center.z);

		private float CalculateCardWidthStep(float tableWidth, int cardsCount) => 
			tableWidth / (cardsCount > 1 ? cardsCount - 1 : 1);
	}
}