using CluckAndCollect.Game.States;
using DG.Tweening;
using UnityEngine;

namespace CluckAndCollect.Behaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class Chicken : MonoBehaviour
    {
        [SerializeField] private AudioClip[] hitSounds;
        
        private AudioSource[] _hits;

        private Transform _transform;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();

            _hits = new AudioSource[hitSounds.Length];
            for (var i = 0; i < hitSounds.Length; i++)
            {
                _hits[i] = gameObject.AddComponent<AudioSource>();
                _hits[i].clip = hitSounds[i];
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Car")) return;
            
            _rigidbody.detectCollisions = false;
            Play.OnDeath.Invoke();
            _transform.DOScaleY(0.1f, 0.1f);
            _hits[Random.Range(0, _hits.Length)].Play();
        }
    }
}