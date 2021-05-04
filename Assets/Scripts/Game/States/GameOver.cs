using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect.Game.States
{
    public class GameOver : GameState
    {
        public static readonly UnityEvent OnEnter = new UnityEvent();
        public static readonly UnityEvent OnExit = new UnityEvent();

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;

        public override void Enter()
        {
            OnEnter.Invoke();
            var gameManager = GameManager.Instance;
            var score = gameManager.LastScore;
            
            scoreText.text = score + " EGGS";

            if (gameManager.IsHighScore(score))
            {
                highScoreText.text = "NEW HIGH SCORE!";
                gameManager.SetHighScore(score);
            }
            else
            {
                highScoreText.text = "HIGH SCORE: " + gameManager.GetHighScore();
            }
        }

        public override void Exit()
        {
            OnExit.Invoke();
        }
    }
}