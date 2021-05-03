using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect
{
    [RequireComponent(typeof(ChickenController))]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public static readonly UnityEvent<GameState> OnStateChange = new UnityEvent<GameState>();
        public GameState CurrentState { get; private set; }

        [SerializeField] private GameState startState;

        public GameData CurrentGameData { get; private set; }

        private bool _ready;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            CameraController.OnFinishTransition.AddListener(() => _ready = true);
        }

        private void Start()
        {
            CurrentState = startState;
            CurrentState.Enter();
            CurrentState.CanvasGroup.alpha = 1;
            CurrentState.CanvasGroup.interactable = CurrentState.CanvasGroup.blocksRaycasts = true;
            _ready = true;
        }

        private void Update()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            if (!_ready)
            {
                return;
            }

            var newState = CurrentState.Tick();

            if (newState != null)
            {
                ChangeState(newState);
            }
        }

        public void ChangeState(GameState newState)
        {
            if (newState == CurrentState)
            {
                return;
            }

            _ready = false;
            CurrentState.Exit();
            newState.Enter();
            OnStateChange.Invoke(newState);
            CurrentState = newState;
        }

        private void NewGame()
        {
            CurrentGameData = new GameData();
        }

        public void Quit()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}