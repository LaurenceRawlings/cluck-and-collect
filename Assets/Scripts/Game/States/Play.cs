using CluckAndCollect.Behaviours;
using CluckAndCollect.Game.Commands;
using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect.Game.States
{
    public class Play : GameState
    {
        public static readonly UnityEvent OnEnter = new UnityEvent();
        public static readonly UnityEvent OnExit = new UnityEvent();
        public static readonly UnityEvent OnMove = new UnityEvent();
        public static readonly UnityEvent OnDeath = new UnityEvent();
        public static readonly UnityEvent OnCoopsFilled = new UnityEvent();
        public static readonly UnityEvent OnTurnFinished = new UnityEvent();

        [SerializeField] private int lives;
        [SerializeField] private int coops;
        [SerializeField] private GameState gameOverState;

        private bool _moveReady;
        private bool _queueReady;
        private int _currentLives;
        private int _filledCoops;

        public override void Enter()
        {
            OnEnter.Invoke();
            _moveReady = true;
            _currentLives = lives;
            _filledCoops = 0;
        }

        public override GameState Tick()
        {
            if (!_moveReady || !_queueReady)
            {
                return null;
            }

            if (_currentLives <= 0)
            {
                return gameOverState;
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

            _moveReady = false;
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
            MoveCommand.OnBadMove.AddListener(Death);
            Chicken.OnHit.AddListener(Death);
            Coop.OnCollect.AddListener(Collect);
            ChickenQueue.OnReady.AddListener(() => _queueReady = true);
        }

        private void Ready()
        {
            _moveReady = true;
        }

        private void Death()
        {
            _queueReady = false;
            _currentLives--;
            OnTurnFinished.Invoke();
            OnDeath.Invoke();
        }

        private void Collect()
        {
            _filledCoops++;

            if (_filledCoops >= coops)
            {
                _currentLives++;
                OnCoopsFilled.Invoke();
            }
            
            OnTurnFinished.Invoke();
        }
    }
}