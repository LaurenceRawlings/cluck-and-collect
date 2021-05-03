using UnityEngine;

namespace CluckAndCollect.Game.States
{
    public abstract class GameState : MonoBehaviour
    {
        [field: SerializeField] public bool OrthographicView { get; private set; }
        [field: SerializeField] public float CameraTransitionDuration { get; private set; }
        [field: SerializeField] public float UIFadeDuration { get; private set; }
        [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }

        [SerializeField] private GameState backState;
        
        public abstract void Enter();

        public virtual GameState Tick()
        {
            return Input.GetButtonDown("Cancel") ? backState : null;
        }

        public abstract void Exit();

        private void Awake()
        {
            CanvasGroup.alpha = 0;
            CanvasGroup.interactable = CanvasGroup.blocksRaycasts = false;
        }
    }
}