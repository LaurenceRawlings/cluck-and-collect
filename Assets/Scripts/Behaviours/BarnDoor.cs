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
            Profiles.OnEnter.AddListener(Open);
            Profiles.OnExit.AddListener(Close);
        }

        private void Open()
        {
            _transform.DOMove(openPosition.position, duration);
        }

        private void Close()
        {
            _transform.DOMove(closedPosition.position, duration);
        }
    }
}