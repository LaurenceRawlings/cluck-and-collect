using System;
using System.Collections;
using UnityEngine;

namespace CluckAndCollect
{
    public class GameState : MonoBehaviour
    {
        [field: SerializeField] public bool OrthographicView { get; private set; }
        [field: SerializeField] public GameStates State { get; private set; }
        
        [SerializeField] private CanvasGroup uI;
        [SerializeField] private float uIFadeDuration;

        private void Start()
        {
            GameManager.Instance.EventManager.onStartSwitchState.AddListener(StartSwitchState);
            GameManager.Instance.EventManager.onFinishSwitchState.AddListener(FinishSwitchState);

            var currentState = GameManager.Instance.CurrentState == this;
            uI.alpha = currentState ? 1 : 0;
            uI.interactable = currentState;
        }

        private void StartSwitchState(GameState state)
        {
            if (state == this)
            {
                return;
            }
            
            StartCoroutine(Fade(uI.alpha, 0));
            uI.interactable = false;
        }

        private void FinishSwitchState()
        {
            if (GameManager.Instance.CurrentState != this)
            {
                return;
            }
            
            StartCoroutine(Fade(uI.alpha, 1));
            uI.interactable = true;
        }

        private IEnumerator Fade(float start, float end)
        {
            var counter = 0f;

            while (counter < uIFadeDuration)
            {
                counter += Time.deltaTime;
                uI.alpha = Mathf.Lerp(start, end, counter / uIFadeDuration);

                yield return null;
            }
        }
    }

    public enum GameStates
    {
        Menu,
        Profiles,
        Settings,
        Leaderboard,
        About,
        Play,
        Replay
    }
}