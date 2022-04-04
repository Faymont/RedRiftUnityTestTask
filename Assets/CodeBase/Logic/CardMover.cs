using CodeBase.Card;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Logic
{
	public class CardMover
	{
		public void MoveCard(CardBase cardObject, Vector3 position, Quaternion rotation)
		{
			MoveWithRotation(cardObject, position, rotation.eulerAngles);
		}

		public void MoveWithRotation(CardBase cardObject, Vector3 position, Vector3 rotation, float duration = 1)
		{
			cardObject.transform.DOKill();
			var sequence = DOTween.Sequence();

			sequence.Insert(0, MoveTween(cardObject, position, duration));
			sequence.Insert(0, RotateTween(cardObject, rotation, duration));

			sequence.OnComplete(() => { cardObject.InMotion = false; });
		}

		public Tween RotateTween(CardBase cardObject, Vector3 rotation, float duration)
		{
			var cardTransform = cardObject.transform;
			cardTransform.DOKill();
			cardObject.InMotion = true;
			return cardTransform.DOLocalRotate(rotation, duration);
		}

		public Tween MoveTween(CardBase cardObject, Vector3 position, float duration)
		{
			var cardTransform = cardObject.transform;
			cardTransform.DOKill();
			cardObject.InMotion = true;
			return cardTransform.DOMove(position, duration);
		}

		public void DeleteTweens(Transform transform)
		{
			transform.DOKill();
		}
	}
}