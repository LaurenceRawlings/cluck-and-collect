using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect
{
    public class MoveCommand : ICommand
    {
        public static readonly UnityEvent OnFinishMove = new UnityEvent();
        public float Time { get; }

        private readonly Vector3 _direction;
        private readonly float _duration;

        public MoveCommand(Vector3 direction, float duration, float time)
        {
            _direction = direction;
            _duration = duration;
            Time = time;
        }

        public void Execute()
        {
            var chicken = GameManager.Instance.ChickenQueue.ActiveChicken;
            var target = chicken.transform.position + (_direction * GameManager.Instance.GridSize);
            var gridLayer = GameManager.Instance.GridLayer;

            if (Physics.CheckSphere(target, 0.05f, gridLayer))
            {
                var grid = Physics.OverlapSphere(target, 0.05f, gridLayer)[0].gameObject;
                chicken.transform.SetParent(grid.transform, true);
            }

            chicken.transform.DOLookAt(target, _duration, AxisConstraint.Y).SetEase(Ease.Linear);
            chicken.transform.DOLocalJump(Vector3.zero, 0.5f, 1, _duration).SetEase(Ease.Linear)
                .OnComplete(() => OnFinishMove.Invoke());
        }
    }
}