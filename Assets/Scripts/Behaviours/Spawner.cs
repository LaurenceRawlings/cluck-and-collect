using System.Collections.Generic;
using System.Linq;
using CluckAndCollect.Game;
using CluckAndCollect.Game.Commands;
using CluckAndCollect.Game.States;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CluckAndCollect.Behaviours
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Vector2 speedBounds;
        [SerializeField] private Vector2 intervalBounds;
        [SerializeField] private Transform target;
        [SerializeField] private List<SpawnItem> prefabs;
        [SerializeField] private bool reversed;

        private float _interval;
        private float _totalWeights;
        private float _time;
        private bool _reverse;
        private bool _enabled;
        private Transform _transform;

        private void Start()
        {
            Play.OnEnter.AddListener(Restart);
            Play.OnExit.AddListener(() => _enabled = false);
            _transform = transform;
            _reverse = reversed;
            _time = Vector3.Distance(_transform.position, target.position) / Mathf.Ceil(Random.Range(speedBounds.x, speedBounds.y));
            _totalWeights = prefabs.Sum(prefab => prefab.Weighting);
            prefabs.Sort();
            _enabled = false;
        }

        private void Update()
        {
            if (!_enabled)
            {
                return;
            }
            
            _interval -= Time.deltaTime;
            if (!(_interval <= 0)) return;
            Spawn();
            ResetInterval();
        }

        private void Restart()
        {
            _enabled = true;
            
            for (var i = 1; i < _transform.childCount; i++)
            {
                Destroy(_transform.GetChild(i).gameObject);
            }
            
            // _reverse = Random.value >= 0.5f;
            ResetInterval();
        }

        private void ResetInterval()
        {
            _interval = Random.Range(intervalBounds.x, intervalBounds.y);
        }

        private GameObject GetRandomPrefab()
        {
            var randomWeight = Random.Range(0, _totalWeights);
            var selected = prefabs.Last().Prefab;

            foreach (var spawn in prefabs)
            {
                if (randomWeight < spawn.Weighting)
                {
                    selected = spawn.Prefab;
                    break;
                }

                randomWeight -= spawn.Weighting;
            }

            return selected;
        }

        private void Spawn()
        {
            var spawnCommand = new SpawnCommand(GetRandomPrefab(), _reverse ? target : _transform,
                _reverse ? _transform : target, _transform, _time);
            
            spawnCommand.Execute();
        }
    }
}