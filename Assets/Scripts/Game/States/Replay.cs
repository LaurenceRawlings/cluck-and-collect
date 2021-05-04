using System.Collections.Generic;
using System.Linq;
using CluckAndCollect.Game.Commands;
using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect.Game.States
{
    public class Replay : GameState
    {
        public static readonly UnityEvent OnEnter = new UnityEvent();
        public static readonly UnityEvent OnExit = new UnityEvent();

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

        public override void Exit()
        {
            OnExit.Invoke();
        }
    }
}