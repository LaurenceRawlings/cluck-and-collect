using System;
using System.Collections.Generic;
using System.Linq;
using CluckAndCollect.Behaviours;
using CluckAndCollect.Game.Commands;
using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect.Game.States
{
    public class Replay : GameState
    {
        public static readonly UnityEvent OnEnter = new UnityEvent();
        public static readonly UnityEvent OnExit = new UnityEvent();

        [SerializeField] private CanvasGroup controls;

        private List<ICommand> _replay;
        private float _startTime;
        private int _currentIndex;

        public override void Enter()
        {
            OnEnter.Invoke();

            _currentIndex = 0;
            _replay = GameManager.Instance.CurrentReplayData.Commands;
            _startTime = Time.time;
            _replay.Sort();
            Play.OnLivesUpdate.Invoke(5);
            Play.OnScoreUpdate.Invoke(0);

            StartCoroutine(CameraController.FadeUI(controls, controls.alpha, 1, 0.2f));
            controls.interactable = controls.blocksRaycasts = true;
        }

        public override GameState Tick()
        {
            if (_currentIndex >= _replay.Count)
            {
                return backState;
            }
            
            var nextCommand = _replay.ElementAt(_currentIndex);

            if (!(nextCommand.Time <= Time.time - _startTime)) return base.Tick();
            
            nextCommand.Execute();
            _currentIndex++;

            return base.Tick();
        }

        private void Start()
        {
            controls.alpha = 0;
            controls.interactable = controls.blocksRaycasts = false;
        }

        public override void Exit()
        {
            OnExit.Invoke();
            Time.timeScale = 1;
            
            StartCoroutine(CameraController.FadeUI(controls, controls.alpha, 0, 0.2f));
            controls.interactable = controls.blocksRaycasts = false;
        }

        public void IncreaseSpeed()
        {
            Time.timeScale += 0.25f;
        }

        public void DecreaseSpeed()
        {
            Time.timeScale -= 0.25f;
        }
        
    }
}