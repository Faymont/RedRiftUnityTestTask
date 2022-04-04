using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Services
{
	public interface IInputProvider :
		IPointerEnterHandler,
		IPointerExitHandler,
		IBeginDragHandler,
		IDragHandler,
		IEndDragHandler
	{
		new Action<PointerEventData> OnBeginDrag { get; set; }
		new Action<PointerEventData> OnDrag { get; set; }
		new Action<PointerEventData> OnEndDrag { get; set; }
		new Action<PointerEventData> OnPointerEnter { get; set; }
		new Action<PointerEventData> OnPointerExit { get; set; }
		Vector2 InputPosition { get; }
	}
}