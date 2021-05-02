using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CluckAndCollect
{
    public class CloudController : MonoBehaviour
    {
        [SerializeField] private Vector2 speedBounds;
        [SerializeField] private Vector2 xBounds;
        [SerializeField] private Vector2 yBounds;
        [SerializeField] private Vector2 sizeBounds;
        [SerializeField] private GameObject[] clouds;
        [SerializeField] private Transform target;
        [SerializeField] private float wait;

        private Transform _transform;
        private Vector3 _position;
        private float _distance;
        private Vector2 _zBounds;

        private void Awake()
        {
            _transform = transform;
            _position = _transform.position;
            _distance = Vector3.Distance(_position, target.transform.position);
        }

        private void Start()
        {
            InvokeRepeating(nameof(SpawnCloud), 0, wait);

            _zBounds = new Vector2(0, 300);
            for (var i = 0; i < 100; i++)
            {
                SpawnCloud();
            }

            _zBounds = Vector2.zero;
        }

        private void SpawnCloud()
        {
            var speed = Random.Range(speedBounds.x, speedBounds.y);
            var size = Random.Range(sizeBounds.x, sizeBounds.y);
            var cloudPrefab = clouds[Random.Range(0, clouds.Length)];

            var time = _distance / speed;
            var spawnPosition = _position + new Vector3(Random.Range(xBounds.x, xBounds.y), Random.Range(yBounds.x, yBounds.y), Random.Range(_zBounds.x, _zBounds.y));
            var targetPosition = spawnPosition + (target.position - _position);

            var cloud = Instantiate(cloudPrefab, spawnPosition, Random.rotation);
            cloud.transform.localScale = new Vector3(size, size, size);
            cloud.transform.DOMove(targetPosition, time).OnComplete(() => Destroy(cloud));
        }
    }
}