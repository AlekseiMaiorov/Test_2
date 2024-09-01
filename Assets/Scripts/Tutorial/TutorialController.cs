using System;
using Cysharp.Threading.Tasks;
using GameStateMachines;
using GameStateMachines.States;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class TutorialController : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        [SerializeField]
        private Button _startTutorial;
        [SerializeField]
        private Button _skipTutorial;
        [SerializeField]
        private Button _endTutorial;
        [SerializeField]
        private GameObject _panelOne;
        [SerializeField]
        private GameObject _panelTwo;
    
    
        public void Init(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        
            _skipTutorial.onClick.AddListener(SkipTutorial);
            _startTutorial.onClick.AddListener(StartTutorial);
            _endTutorial.onClick.AddListener(EndTutorial);
        }

        private void EndTutorial()
        {
            _gameStateMachine.Enter<BattleState>().Forget();
            _panelTwo.SetActive(false);
        }
    
        private void StartTutorial()
        {
            _panelOne.SetActive(false);
            _panelTwo.SetActive(true);
        }

        private void SkipTutorial()
        {
            _gameStateMachine.Enter<BattleState>().Forget();
            _panelOne.SetActive(false);
        }

        public void OnDestroy()
        {
            _skipTutorial.onClick.RemoveAllListeners();
            _startTutorial.onClick.RemoveAllListeners();
            _endTutorial.onClick.RemoveAllListeners();
        }
    }
}
