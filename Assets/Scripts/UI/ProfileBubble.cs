using CluckAndCollect.Game;
using TMPro;
using UnityEngine;

namespace CluckAndCollect.UI
{
    [RequireComponent(typeof(TextMeshPro))]
    public class ProfileBubble : MonoBehaviour
    {
        private TextMeshPro _tmp;
        private void Awake()
        {
            _tmp = GetComponent<TextMeshPro>();
        }

        private void Start()
        {
            GameManager.OnProfileChange.AddListener((player) => _tmp.text = "WELCOME PLAYER " + player);
        }
    }
}