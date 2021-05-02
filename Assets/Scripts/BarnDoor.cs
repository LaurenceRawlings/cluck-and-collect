using System;
using DG.Tweening;
using UnityEngine;

namespace CluckAndCollect
{
    public class BarnDoor : MonoBehaviour
    {
        [SerializeField] private Transform openPosition;
        [SerializeField] private Transform closedPosition;
        [SerializeField] private float duration;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            GameManager.Instance.EventManager.onStartSwitchState.AddListener(Open);
            GameManager.Instance.EventManager.onFinishSwitchState.AddListener(Close);
        }

        private void Open(GameState state)
        {
            if (state.State == GameStates.Profiles)
            {
                _transform.DOMove(openPosition.position, duration);
            }
        }

        private void Close()
        {
            if (GameManager.Instance.CurrentState.State == GameStates.Menu)
            {
                _transform.DOMove(closedPosition.position, duration);
            }
        }
    }
}