using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CluckAndCollect
{
    [RequireComponent(typeof(ChickenController))]
    [RequireComponent(typeof(EventManager))]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public EventManager EventManager { get; private set; }
        public GameState CurrentState { get; private set; }
        public float GridSize { get; private set; }

        [SerializeField] private GameState startState;

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

            EventManager = GetComponent<EventManager>();
            CurrentState = startState;
        }

        private void Start()
        {
            EventManager.onStartSwitchState.AddListener(SwitchState);
        }

        private void SwitchState(GameState state)
        {
            CurrentState = state;
        }
    }
}