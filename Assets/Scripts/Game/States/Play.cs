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
        public static readonly UnityEvent<int> OnLivesUpdate = new UnityEvent<int>();
        public static readonly UnityEvent<int> OnScoreUpdate = new UnityEvent<int>();

        [SerializeField] private int lives;
        [SerializeField] private int coops;
        [SerializeField] private GameState gameOverState;
        [SerializeField] private CanvasGroup[] tutorialPanes;
        [SerializeField] private CanvasGroup tutorialCanvas;

        private bool _moveReady;
        private bool _queueReady;
        private int _currentLives;
        private int _filledCoops;
        private int _score;
        private bool[] _shownTutorial;
        private int _moves;

        public override void Enter()
        {
            OnEnter.Invoke();
            _moveReady = true;
            _currentLives = lives;
            _filledCoops = 0;
            _score = 0;
            _shownTutorial = new bool[tutorialPanes.Length];
            _moves = 0;

            ShowTutorial(0);
        }

        private void ShowTutorial(int tutorial)
        {
            if (!GameManager.Instance.ShowTutorial() || _shownTutorial[tutorial])
            {
                return;
            }

            _shownTutorial[tutorial] = true;
            var canvasGroup = tutorialPanes[tutorial];
            StartCoroutine(CameraController.FadeUI(canvasGroup, canvasGroup.alpha, 1, 1f));

            if (tutorialCanvas.alpha < 1)
            {
                StartCoroutine(CameraController.FadeUI(tutorialCanvas, tutorialCanvas.alpha, 1, 1f));
            }
        }
        
        private void HideTutorials()
        {
            foreach (var canvasGroup in tutorialPanes)
            {
                StartCoroutine(CameraController.FadeUI(canvasGroup, canvasGroup.alpha, 0, 1f));
            }
            
            StartCoroutine(CameraController.FadeUI(tutorialCanvas, tutorialCanvas.alpha, 0, 1f));
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

            _moves++;

            if (_moves > 5)
            {
                ShowTutorial(1);
            }

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
            HideTutorials();
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
            OnLivesUpdate.Invoke(_currentLives);
        }

        private void Collect()
        {
            _filledCoops++;
            _score++;
            
            if (!_shownTutorial[2])
            {
                ShowTutorial(2);
            } 
            else if (!_shownTutorial[3])
            {
                ShowTutorial(3);
            }
            else if (!_shownTutorial[4])
            {
                ShowTutorial(4);
                Invoke(nameof(HideTutorials), 5f);
            }

            if (_filledCoops >= coops)
            {
                _currentLives++;
                _score += coops;
                OnCoopsFilled.Invoke();
                OnLivesUpdate.Invoke(_currentLives);
            }
            
            OnScoreUpdate.Invoke(_score);
            OnTurnFinished.Invoke();
        }
    }
}