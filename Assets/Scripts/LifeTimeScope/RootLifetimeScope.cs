using Enemy;
using EntryPoint;
using Factories;
using GameStateMachines;
using GameStateMachines.States;
using Player;
using Services.AssetManagement;
using Services.SceneLoader;
using UI.Presenter;
using VContainer;
using VContainer.Unity;

namespace LifeTimeScope
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameEntryPoint>();
            builder.Register<PlayerInput>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<HealthPresenter>(Lifetime.Transient);
            builder.Register<PlayerFactory>(Lifetime.Singleton);
            builder.Register<BattleUIFactory>(Lifetime.Singleton);
            builder.Register<EnemiesFactory>(Lifetime.Singleton);
            builder.Register<EnemiesPool>(Lifetime.Singleton);
            builder.Register<EnemySpawner>(Lifetime.Singleton);
            RegisterGameStateMachine(builder);
            RegisterServices(builder);
        }

        private void RegisterGameStateMachine(IContainerBuilder builder)
        {
            builder.Register<GameStateMachine>(Lifetime.Singleton);
            builder.Register<BootstrapState>(Lifetime.Singleton);
            builder.Register<TutorialState>(Lifetime.Singleton);
            builder.Register<BattleState>(Lifetime.Singleton);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<SimpleAssetLoader>(Lifetime.Singleton);
            builder.Register<AddressablesSceneLoader>(Lifetime.Singleton);
        }
    }
}