using Common.StateMachine;
using Cysharp.Threading.Tasks;
using Enemy;

namespace GameStateMachines.States
{
    public class BattleState : State
    {
        private EnemySpawner _enemySpawner;

        public BattleState(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }

        public override UniTask Enter()
        {
            _enemySpawner.StartSpawn().Forget();
            return UniTask.CompletedTask;
        }

        public override UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}