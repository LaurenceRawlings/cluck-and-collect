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
            for (var i = 0; i < _jumps.Length; i++)
            {
                _jumps[i] = gameObject.AddComponent<AudioSource>();
                _jumps[i].clip = jumpSounds[i];
            }

            _backgrounds = new AudioSource[backgroundSounds.Length];
            for (var i = 0; i < _backgrounds.Length; i++)
            {
                _backgrounds[i] = gameObject.AddComponent<AudioSource>();
                _backgrounds[i].clip = backgroundSounds[i];
                _backgrounds[i].loop = true;
                _backgrounds[i].Play();
            }
            
            UpdateVolume();
            
            Play.OnDeath.AddListener(Death);
            Play.OnMove.AddListener(Move);
            Settings.OnSettingsChange.AddListener(UpdateVolume);
        }
        
        private void Move()
        {
            _jumps[Random.Range(0, _jumps.Length)].Play();
        }

        private void Death()
        {
            _death.Play();
        }

        private void UpdateVolume()
        {
            var ambianceVolume = PlayerPrefs.HasKey("ambiance") ? PlayerPrefs.GetFloat("ambiance") : 1;
            var effectsVolume = PlayerPrefs.HasKey("effects") ? PlayerPrefs.GetFloat("effects") : 1;
            
            _death.volume = effectsVolume;

            for (var i = 0; i < _jumps.Length; i++)
            {
                _jumps[i].volume = effectsVolume;
            }

            for (var i = 0; i < _backgrounds.Length; i++)
            {
                _backgrounds[i].volume = ambianceVolume;
            }
        }
    }
}