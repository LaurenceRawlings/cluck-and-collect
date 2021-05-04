using CluckAndCollect.Game.States;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect.Game.Commands
{
    public class MoveCommand : ICommand
    {
        public static readonly UnityEvent OnFinishMove = new UnityEvent();
        public static readonly UnityEvent OnBadMove = new UnityEvent();
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

            var gridSquare = new Collider[1];
            var wall = new Collider[1];

            var gridCollision = Physics.OverlapBoxNonAlloc(
                target, new Vector3(0.75f, 0.25f, 0.75f),
                gridSquare, Quaternion.identity, GameManager.Instance.GridLayer) > 0;
            var wallCollision = Physics.OverlapBoxNonAlloc(target, new Vector3(0.75f, 0.25f, 0.75f),
                wall, Quaternion.identity, GameManager.Instance.WallLayer) > 0;

            chicken.transform.DOLookAt(target, _duration, AxisConstraint.Y).SetEase(Ease.Linear);

            if (wallCollision || gridCollision)
            {
                // Grid square or wall

                if (!gridCollision)
                {
                    target = chicken.transform.position;
                }
                else
                {
                    chicken.transform.SetParent(gridSquare[0].gameObject.transform, true);
                }
                
                chicken.transform.DOLocalJump(Vector3.zero, 0.5f, 1, _duration).SetEase(Ease.Linear)
                    .OnComplete(() => OnFinishMove.Invoke());
            }
            else
            {
                // River
                
                var sequence = DOTween.Sequence().SetEase(Ease.Linear);
                sequence.Append(chicken.transform.DOJump(target, 0.5f, 1, _duration));
                sequence.Append(chicken.transform
                    .DOMove(target + (Vector3.down * GameManager.Instance.GridSize), _duration));
                sequence.OnComplete(() =>
                {
                    Object.Destroy(chicken);
                    OnFinishMove.Invoke();
                });
                OnBadMove.Invoke();
            }
        }
    }
}