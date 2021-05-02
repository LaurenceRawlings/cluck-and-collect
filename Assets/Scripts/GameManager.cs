using System;
using UnityEngine;

namespace CluckAndCollect
{
    [RequireComponent(typeof(MenuController))]
    [RequireComponent(typeof(ChickenController))]
    [RequireComponent(typeof(EventManager))]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public EventManager EventManager { get; private set; }
        public GameState CurrentState { get; private set; }

        [SerializeField] private GameState startState;

        private MenuController _menuController;

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

            _menuController = GetComponent<MenuController>();
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