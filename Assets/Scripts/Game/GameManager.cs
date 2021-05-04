using CluckAndCollect.Behaviours;
using CluckAndCollect.Game.States;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect.Game
{
    [RequireComponent(typeof(ChickenQueue))]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public static readonly UnityEvent<GameState> OnStateChange = new UnityEvent<GameState>();
        public static readonly UnityEvent<int> OnProfileChange = new UnityEvent<int>();
        public static readonly UnityEvent OnProfileUpdate = new UnityEvent();

        [field: SerializeField] public float GridSize { get; private set; }
        [field: SerializeField] public LayerMask GridLayer { get; private set; }
        [field: SerializeField] public LayerMask WallLayer { get; private set; }
        [field: SerializeField] public float MoveDuration { get; private set; }
        [field: SerializeField] public float MoveDelay { get; private set; }
        public GameState CurrentState { get; private set; }
        public GameData CurrentGameData { get; private set; }
        public ChickenQueue ChickenQueue { get; private set; }

        [SerializeField] private GameState startState;

        private bool _ready;
        private int _profile;

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
            ChickenQueue = GetComponent<ChickenQueue>();
        }

        private void Start()
        {
            CurrentState = startState;
            CurrentState.Enter();
            CurrentState.CanvasGroup.alpha = 1;
            CurrentState.CanvasGroup.interactable = CurrentState.CanvasGroup.blocksRaycasts = true;
            _ready = true;
            SwitchProfile(1);
            DOTween.SetTweensCapacity(1000, 50);
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

            if (newState)
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

        public void SwitchProfile(int profile)
        {
            _profile = profile;
            OnProfileChange.Invoke(profile);
        }

        public static int GetHighScore(int profile)
        {
            return PlayerPrefs.GetInt("score" + profile);
        }

        public static void ResetHighScore(int profile)
        {
            SetHighScore(profile, 0);
        }

        public static void SetHighScore(int profile, int score)
        {
            PlayerPrefs.SetInt("score" + profile, score);
            PlayerPrefs.Save();
            OnProfileUpdate.Invoke();
        }
    }
}