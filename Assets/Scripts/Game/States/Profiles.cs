using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect.Game.States
{
    public class Profiles : GameState
    {
        public static readonly UnityEvent OnEnter = new UnityEvent();
        public static readonly UnityEvent OnExit = new UnityEvent();

        [SerializeField] private TextMeshProUGUI[] profileHighScores;

        private void Start()
        {
            GameManager.OnProfileUpdate.AddListener(UpdateScores);
        }

        public override void Enter()
        {
            OnEnter.Invoke();
            UpdateScores();
        }

        public override void Exit()
        {
            OnExit.Invoke();
        }

        private void UpdateScores()
        {
            for (var i = 0; i < profileHighScores.Length; i++)
            {
                profileHighScores[i].text = GameManager.GetHighScore(i + 1).ToString();
            }
        }
    }
}