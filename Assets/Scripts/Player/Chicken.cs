using System;
using DG.Tweening;
using UnityEngine;

namespace CluckAndCollect
{
    public class Chicken : MonoBehaviour, IEntity
    {
        private Transform _transform;
        
        [SerializeField]
        private float gridSize;
        
        private void Awake()
        {
            _transform = transform;
        }

        public void Move(Vector3 direction)
        {
            var position = _transform.position;

            _transform.DOLookAt(position + direction, 0.1f);
            _transform.DOJump(position + (direction * gridSize), 1, 1, 0.5f);
        }
    }
}