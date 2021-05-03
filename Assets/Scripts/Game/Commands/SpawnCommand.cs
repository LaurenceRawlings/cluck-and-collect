using DG.Tweening;
using UnityEngine;

namespace CluckAndCollect
{
    public class SpawnCommand : ICommand
    {
        public float Time { get; }

        private readonly GameObject _prefab;
        private readonly Transform _position;
        private readonly Transform _target;
        private readonly float _duration;

        public SpawnCommand(GameObject prefab, Transform position, Transform target, float duration, float time)
        {
            _prefab = prefab;
            _position = position;
            _target = target;
            _duration = duration;
            Time = time;
        }

        public void Execute()
        {
            var spawned = Object.Instantiate(_prefab, _position.position, _position.rotation);
            spawned.transform.DOMove(_target.position, _duration).SetEase(Ease.Linear)
                .OnComplete(() => Object.Destroy(spawned));
        }
    }
}