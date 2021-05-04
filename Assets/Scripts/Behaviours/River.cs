using System;
using CluckAndCollect.Game.States;
using UnityEngine;

namespace CluckAndCollect.Behaviours
{
    public class River : MonoBehaviour
    {
        [SerializeField] private ParticleSystem splash;
        [SerializeField] private AudioClip splashSound;

        private AudioSource _source;
        
        private void Awake()
        {
            _source = gameObject.AddComponent<AudioSource>();
            _source.clip = splashSound;
            UpdateVolume();
        }

        private void Start()
        {
            Settings.OnSettingsChange.AddListener(UpdateVolume);
        }

        private void OnTriggerEnter(Collider other)
        {
            Instantiate(splash, other.transform.position, Quaternion.identity);
            _source.Play();
        }

        private void UpdateVolume()
        {
            _source.volume = PlayerPrefs.GetFloat("effects");
        }
    }
}