using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect
{
    public class Menu : GameState
    {
        public static readonly UnityEvent OnEnter = new UnityEvent();
        public static readonly UnityEvent OnExit = new UnityEvent();

        public override void Enter()
        {
            OnEnter.Invoke();
        }

        public override GameState Tick()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                GameManager.Instance.Quit();
            }

            return null;
        }

        public override void Exit()
        {
            OnExit.Invoke();
        }
    }
}