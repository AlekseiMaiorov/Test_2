using Common.StateMachine;
using GameStateMachines.States;

namespace GameStateMachines
{
    public class GameStateMachine : StateMachine
    {
        public GameStateMachine(BootstrapState bootstrapState, TutorialState tutorialState, BattleState battleState) :
            base(states: new IExitableState[] {bootstrapState, tutorialState, battleState})
        {
        }
    }
}