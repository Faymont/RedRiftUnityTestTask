using CodeBase.Card;
using CodeBase.GamePlay;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Services;
using Zenject;

namespace CodeBase.Infrastructure
{
	public class GameInstaller : MonoInstaller
	{
		public CardObject cardPrefab;
		public CardDragger cardDragger;
		public CardEngine cardEngine;

		public override void InstallBindings()
		{
			Container.Bind<LoadImageService>().AsSingle();
			Container.Bind<CardMover>().AsSingle();
			Container.Bind<CardDragger>().FromInstance(cardDragger).AsSingle();
			Container.BindFactory<CardObject, CardFactory>().FromComponentInNewPrefab(cardPrefab);
			Container.Bind<CardEngine>().FromInstance(cardEngine).AsSingle();
		}
	}
}