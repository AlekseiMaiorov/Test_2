using Cysharp.Threading.Tasks;
using GameStateMachines;
using GameStateMachines.States;
using VContainer.Unity;

namespace EntryPoint
{
    public class GameEntryPoint : IInitializable
    {
        private readonly GameStateMachine _gameStateMachine;

        public GameEntryPoint(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            _gameStateMachine.Enter<BootstrapState>().Forget();
        }
    }
}