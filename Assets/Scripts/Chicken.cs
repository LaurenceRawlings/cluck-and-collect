using System;
using UnityEngine;

namespace CluckAndCollect
{
    public class Chicken : MonoBehaviour
    {
        private Transform _transform;
        private GameManager _gameManager;
        private float _gridSize;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _gameManager.EventManager.onMoveCommand.AddListener(Move);
            _gridSize = _gameManager.GridSize;
        }

        private void Move(Vector3 direction)
        {
        }
    }
}