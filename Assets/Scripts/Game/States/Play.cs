using CluckAndCollect.Game.Commands;
using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect.Game.States
{
    public class Play : GameState
    {
        public static readonly UnityEvent OnEnter = new UnityEvent();
        public static readonly UnityEvent OnExit = new UnityEvent();
        public static readonly UnityEvent OnDeath = new UnityEvent();
        public static readonly UnityEvent OnMove = new UnityEvent();

        private bool _ready;

        public override void Enter()
        {
            OnEnter.Invoke();
            _ready = true;
        }

        public override GameState Tick()
        {
            if (!_ready)
            {
                return null;
            }
            
            var direction = Vector3.zero;
            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");

            if (Mathf.Abs(vertical) > 0.05f)
            {
                direction = vertical > 0 ? Vector3.forward : Vector3.back;
            }
            else if (Mathf.Abs(horizontal) > 0.05f)
            {
                direction = horizontal > 0 ? Vector3.right : Vector3.left;
            }

            if (direction == Vector3.zero) return null;

            _ready = false;
            var moveCommand = new MoveCommand(direction, GameManager.Instance.MoveDuration, Time.time);
            moveCommand.Execute();
            OnMove.Invoke();

            return base.Tick();
        }

        public override void Exit()
        {
            OnExit.Invoke();
        }

        private void Start()
        {
            MoveCommand.OnFinishMove.AddListener(() => Invoke(nameof(Ready), GameManager.Instance.MoveDelay));
        }

        private void Ready()
        {
            _ready = true;
        }
    }
}