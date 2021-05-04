using TMPro;
using UnityEngine;

namespace CluckAndCollect.UI
{
    public class Play : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI livesText;
        [SerializeField] private TextMeshProUGUI scoreText;

        private void Start()
        {
            Game.States.Play.OnScoreUpdate.AddListener(UpdateScore);
            Game.States.Play.OnLivesUpdate.AddListener(UpdateLives);
        }

        private void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }

        private void UpdateLives(int lives)
        {
            livesText.text = lives.ToString();
        }
    }
}