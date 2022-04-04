using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Services
{
	[RequireComponent(typeof(Collider))]
	public class InputProvider : MonoBehaviour, IInputProvider
	{
		void Awake()
		{
			if (Camera.main.GetComponent<PhysicsRaycaster>() == null)
				throw new Exception(GetType() + " needs an " + typeof(PhysicsRaycaster) + " on the MainCamera");
		}

		public Action<PointerEventData> OnBeginDrag { get; set; }
		public Action<PointerEventData> OnDrag { get; set; }
		public Action<PointerEventData> OnEndDrag { get; set; }
		public Action<PointerEventData> OnPointerEnter { get; set; }
		public Action<PointerEventData> OnPointerExit { get; set; }
		public Vector2 InputPosition { get; }

		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) =>
			((IInputProvider)this).OnPointerEnter?.Invoke(eventData);

		void IPointerExitHandler.OnPointerExit(PointerEventData eventData) =>
			((IInputProvider)this).OnPointerExit?.Invoke(eventData);

		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) =>
			((IInputProvider)this).OnBeginDrag?.Invoke(eventData);

		void IDragHandler.OnDrag(PointerEventData eventData) =>
			((IInputProvider)this).OnDrag?.Invoke(eventData);

		void IEndDragHandler.OnEndDrag(PointerEventData eventData) =>
			((IInputProvider)this).OnEndDrag?.Invoke(eventData);
	}
}