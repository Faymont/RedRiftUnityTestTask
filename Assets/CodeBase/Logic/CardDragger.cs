using CodeBase.Card;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic
{
	public class CardDragger : MonoBehaviour
	{
		[SerializeField] private Camera mainCamera;
		[SerializeField] private float cameraHeight = 20f;
		[SerializeField] private float dragHeight = 10f;
		[SerializeField] private CardBase currentCardObject;

		private CardMover _cardMover;

		[Inject]
		public void Construct(CardMover cardMover)
		{
			_cardMover = cardMover;
		}

		public void Update()
		{
			FollowCursor();
		}

		private void FollowCursor()
		{
			if (currentCardObject != null)
			{
				var worldPosition = WorldPosition();
				worldPosition.y = dragHeight;
				currentCardObject.transform.position = worldPosition;
			}
		}

		public void BeginDrag(CardBase cardObject)
		{
			currentCardObject = cardObject;
			currentCardObject.IsDragging = true;

			_cardMover.DeleteTweens(cardObject.transform);
		}

		public void Release()
		{
			if (currentCardObject != null)
			{
				currentCardObject.IsDragging = false;
				currentCardObject = null;
			}
		}

		private Vector3 WorldPosition()
		{
			var mousePos = Input.mousePosition;
			mousePos.z = cameraHeight;
			return mainCamera.ScreenToWorldPoint(mousePos);
		}
	}
}