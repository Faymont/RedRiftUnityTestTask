using CodeBase.Card;
using CodeBase.Extensions;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
	public class CardFactory : PlaceholderFactory<CardObject>
	{
		public override CardObject Create()
		{
			var card = base.Create();

			var cardState = CardMics.GetRandomCardData();

			card.Setup(cardState);

			return card;
		}
	}
}