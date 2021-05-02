using UnityEngine;

namespace CluckAndCollect
{
    public class InputManager : MonoBehaviour
    {
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameManager.Instance;
        }

        private void Update()
        {
            var currentState = _gameManager.CurrentState;

            if (Input.GetButtonDown("Cancel"))
            {
                if (currentState.BackState)
                {
                    _gameManager.EventManager.onStartSwitchState.Invoke(currentState.BackState);
                }
            }
        }
    }
}