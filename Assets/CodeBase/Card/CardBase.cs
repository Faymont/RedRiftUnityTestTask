using System;
using System.Collections.Generic;
using CodeBase.Card.Data;
using CodeBase.Logic;
using CodeBase.Services;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Card
{
	public abstract class CardBase : MonoBehaviour
	{
		[SerializeField] private CardVisuals cardVisuals;

		public CardVisuals CardVisuals => cardVisuals;
		public ICardHolder CurrentPile { get; set; }
		public List<ICardValue> CardValues { get; set; } = new List<ICardValue>();

		public bool InMotion { get; set; }
		public bool IsDragging { get; set; }
		public bool CanPlay { get; set; }
		public bool Played { get; set; }

		public event Action<CardBase> OnPlayed;
		public event Action<CardBase> OnSelected;
		public event Action<CardBase> OnUnselected;
		public event Action<CardBase> OnDragBegan;
		public event Action<CardBase> OnDragged;
		public event Action<CardBase> OnDragEnded;
		public event Action<CardData> OnDataLoaded;

		private IInputProvider _inputProvider;

		protected virtual void Awake()
		{
			_inputProvider = GetComponent<IInputProvider>();
		}

		protected virtual void OnEnable()
		{
			_inputProvider.OnPointerEnter += Select;
			_inputProvider.OnPointerExit += Unselect;
			_inputProvider.OnBeginDrag += OnBeginDrag;
			_inputProvider.OnDrag += OnDrag;
			_inputProvider.OnEndDrag += OnEndDrag;
		}

		protected virtual void OnDisable()
		{
			_inputProvider.OnPointerEnter -= Select;
			_inputProvider.OnPointerExit -= Unselect;
			_inputProvider.OnBeginDrag -= OnBeginDrag;
			_inputProvider.OnDrag -= OnDrag;
			_inputProvider.OnEndDrag -= OnEndDrag;
		}

		public virtual void Setup(CardData cardData)
		{
		}

		protected virtual void OnBeginDrag(PointerEventData obj) =>
			OnDragBegan?.Invoke(this);

		protected virtual void OnDrag(PointerEventData obj) =>
			OnDragged?.Invoke(this);

		protected virtual void OnEndDrag(PointerEventData obj) =>
			OnDragEnded?.Invoke(this);

		protected virtual void Unselect(PointerEventData obj) =>
			OnUnselected?.Invoke(this);

		protected virtual void Select(PointerEventData obj) =>
			OnSelected?.Invoke(this);

		protected virtual void Play() =>
			OnPlayed?.Invoke(this);

		protected virtual void LoadData(CardData cardData)
		{
			OnDataLoaded?.Invoke(cardData);
		}
	}
}