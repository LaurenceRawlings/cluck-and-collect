using CluckAndCollect.Game.States;
using DG.Tweening;
using UnityEngine;

namespace CluckAndCollect.Behaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class Chicken : MonoBehaviour
    {
        private Transform _transform;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Car")) return;
            
            _rigidbody.detectCollisions = false;
            Play.OnDeath.Invoke();
            _transform.DOScaleY(0.1f, 0.1f);
        }
    }
}