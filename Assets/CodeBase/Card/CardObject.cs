using CodeBase.Card.Data;
using CodeBase.GamePlay;
using CodeBase.Logic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.Card
{
	[RequireComponent(typeof(CardHealth))]
	[RequireComponent(typeof(CardMana))]
	[RequireComponent(typeof(CardPower))]
	public class CardObject : CardBase
	{
		public CardHealth cardHealth;
		public CardPower cardPower;
		public CardMana cardMana;

		private CardEngine _cardEngine;
		private CardDragger _cardDragger;
		private CardData _cardData;

		[Inject]
		public void Construct(CardDragger cardDragger, CardEngine cardEngine)
		{
			_cardDragger = cardDragger;
			_cardEngine = cardEngine;
		}

		protected override void OnEnable()
		{
			base.OnEnable();

			cardHealth.ValueChanged += HealthChanged;
		}

		public override void Setup(CardData cardData)
		{
			_cardData = cardData;

			CardValues.Add(cardHealth);
			CardValues.Add(cardPower);
			CardValues.Add(cardMana);

			LoadData(cardData);
		}

		protected override void LoadData(CardData cardData)
		{
			cardHealth.LoadState(cardData);
			cardPower.LoadState(cardData);
			cardMana.LoadState(cardData);

			base.LoadData(cardData);
		}

		protected override void OnBeginDrag(PointerEventData obj)
		{
			if (Played)
				return;

			_cardDragger.BeginDrag(this);

			base.OnBeginDrag(obj);
		}

		protected override void OnEndDrag(PointerEventData obj)
		{
			_cardDragger.Release();

			if (CanPlay)
			{
				Play();
			}

			base.OnEndDrag(obj);
		}

		protected override void Play()
		{
			Played = true;

			_cardEngine.PlayCard(this);

			base.Play();
		}

		private void HealthChanged(int value)
		{
			if (value <= 0)
			{
				_cardEngine.DiscardCard(this);
			}
		}
	}
}