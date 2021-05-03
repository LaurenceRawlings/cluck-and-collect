using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CluckAndCollect
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Vector2 speedBounds;
        [SerializeField] private Vector2 intervalBounds;
        [SerializeField] private Transform target;
        [SerializeField] private List<SpawnItem> prefabs;

        private float _interval;
        private float _totalWeights;
        private float _time;
        private bool _reverse;
        private GameManager _gameManager;
        private Transform _transform;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _transform = transform;
            Restart();
        }

        private void Update()
        {
            _interval -= Time.deltaTime;
            if (!(_interval <= 0)) return;
            Spawn();
            ResetInterval();
        }

        private void Restart()
        {
            for (var i = 1; i < _transform.childCount; i++)
            {
                Destroy(_transform.GetChild(i).gameObject);
            }
            
            _reverse = Random.value >= 0.5f;
            _totalWeights = prefabs.Sum(prefab => prefab.Weighting);
            _time = Vector3.Distance(_transform.position, target.position) / Random.Range(speedBounds.x, speedBounds.y);
            prefabs.Sort();
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
                _reverse ? _transform : target, _time, Time.time);
            
            spawnCommand.Execute();
        }
    }
}