﻿using UnityEngine.Events;

namespace CluckAndCollect.Game.States
{
    public class Profiles : GameState
    {
        public static readonly UnityEvent OnEnter = new UnityEvent();
        public static readonly UnityEvent OnExit = new UnityEvent();

        public override void Enter()
        {
            OnEnter.Invoke();
        }

        public override void Exit()
        {
            OnExit.Invoke();
        }
    }
}