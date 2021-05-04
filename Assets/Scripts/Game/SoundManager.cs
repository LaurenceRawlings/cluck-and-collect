using System;
using CluckAndCollect.Game.States;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CluckAndCollect.Game
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip deathSound;
        [SerializeField] private AudioClip[] jumpSounds;
        [SerializeField] private AudioClip[] backgroundSounds;

        private AudioSource _death;
        private AudioSource[] _jumps;
        private AudioSource[] _backgrounds;

        private void Awake()
        {
            _death = gameObject.AddComponent<AudioSource>();
            _death.clip = deathSound;

            _jumps = new AudioSource[jumpSounds.Length];
            for (var i = 0; i < jumpSounds.Length; i++)
            {
                _jumps[i] = gameObject.AddComponent<AudioSource>();
                _jumps[i].clip = jumpSounds[i];
            }

            _backgrounds = new AudioSource[backgroundSounds.Length];
            for (var i = 0; i < backgroundSounds.Length; i++)
            {
                _backgrounds[i] = gameObject.AddComponent<AudioSource>();
                _backgrounds[i].clip = backgroundSounds[i];
                _backgrounds[i].loop = true;
                _backgrounds[i].volume = 0.2f;
                _backgrounds[i].Play();
            }
            
            Play.OnDeath.AddListener(Death);
            Play.OnMove.AddListener(Move);
        }
        
        private void Move()
        {
            _jumps[Random.Range(0, _jumps.Length)].Play();
        }

        private void Death()
        {
            _death.Play();
        }
    }
}