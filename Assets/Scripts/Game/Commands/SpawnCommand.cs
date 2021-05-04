using DG.Tweening;
using UnityEngine;

namespace CluckAndCollect.Game.Commands
{
    public class SpawnCommand : ICommand
    {
        public float Time { get; }

        private readonly GameObject _prefab;
        private readonly Transform _position;
        private readonly Transform _target;
        private readonly Transform _parent;
        private readonly float _duration;

        public SpawnCommand(GameObject prefab, Transform position, Transform target, Transform parent, float duration)
        {
            _prefab = prefab;
            _position = position;
            _target = target;
            _parent = parent;
            _duration = duration;
            Time = UnityEngine.Time.time - GameManager.Instance.CurrentReplayData.StartTime;
            GameManager.Instance.CurrentReplayData.Commands.Add(this);
        }

        public void Execute()
        {
            var spawned = Object.Instantiate(_prefab, _position.position, _position.rotation, _parent);
            spawned.transform.DOMove(_target.position, _duration).SetEase(Ease.Linear)
                .OnComplete(() => Object.Destroy(spawned));
        }
        
        public int CompareTo(ICommand other)
        {
            return Time.CompareTo(other.Time);
        }
    }
}