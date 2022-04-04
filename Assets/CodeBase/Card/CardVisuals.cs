using CodeBase.Card.Data;
using CodeBase.Extensions;
using CodeBase.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace CodeBase.Card
{
	public class CardVisuals : MonoBehaviour
	{
		[Header("Main card visuals")]
		[SerializeField] private CardBase card;
		[SerializeField] private SpriteRenderer mainImage;
		[SerializeField] private GameObject cardOutline;

		[Header("Card data")]
		[SerializeField] private TMP_Text cardNameText;
		[SerializeField] private TMP_Text cardDescriptionText;
		[SerializeField] private CardHealth cardHealth;
		[SerializeField] private TMP_Text healthText;
		[SerializeField] private CardPower cardPower;
		[SerializeField] private TMP_Text manaText;
		[SerializeField] private CardMana cardMana;
		[SerializeField] private TMP_Text powerText;

		[Header("Sorting")]
		[SerializeField] private SortingGroup sortingGroup;
		[SerializeField] private int dragSortOrderModifier;

		private LoadImageService _loadImageService;
		private int _startSortOrder;

		[Inject]
		public void Construct(LoadImageService loadImageService)
		{
			_loadImageService = loadImageService;
		}

		private void OnEnable()
		{
			card.OnDragBegan += OnDragStart;
			card.OnDragEnded += OnDragEnded;
			card.OnDataLoaded += OnDataLoaded;
			cardHealth.ValueChanged += UpdateHealth;
			cardPower.ValueChanged += UpdatePower;
			cardMana.ValueChanged += UpdateMana;
		}

		private void OnDisable()
		{
			card.OnDragBegan -= OnDragStart;
			card.OnDragEnded -= OnDragEnded;
			card.OnDataLoaded -= OnDataLoaded;
			cardHealth.ValueChanged -= UpdateHealth;
			cardPower.ValueChanged -= UpdatePower;
			cardMana.ValueChanged -= UpdateMana;
		}

		public int GetSortOrder() =>
			sortingGroup.sortingOrder;

		public void SetCardName(string cardName) =>
			cardNameText.text = cardName;

		public void SetDescription(string cardDescription) =>
			cardDescriptionText.text = cardDescription;

		public void ChangeSortOrder(int order) =>
			sortingGroup.sortingOrder = order;

		private void OnDataLoaded(CardData data) => 
			LoadNewTexture(data.URL);

		public void LoadNewTexture(string url) =>
			_loadImageService.DownloadImage(url, OnLoad);

		private void OnDragStart(CardBase obj)
		{
			cardOutline.SetActive(true);
			_startSortOrder = GetSortOrder();
			ChangeSortOrder(_startSortOrder + dragSortOrderModifier);
		}

		private void OnDragEnded(CardBase obj)
		{
			cardOutline.SetActive(false);
			ChangeSortOrder(_startSortOrder);
		}

		private void UpdateHealth(int value) =>
			ChangeTextValue(healthText, value);

		private void UpdatePower(int value) =>
			ChangeTextValue(powerText, value);

		private void UpdateMana(int value) =>
			ChangeTextValue(manaText, value);

		private void OnLoad(Texture2D texture)
		{
			var sprite = texture.ConvertToSprite();
			mainImage.sprite = sprite;
		}

		private void ChangeTextValue(TMP_Text text, int to, float duration = 1)
		{
			text.DOTextIntCounter(to, duration);
		}
	}
}