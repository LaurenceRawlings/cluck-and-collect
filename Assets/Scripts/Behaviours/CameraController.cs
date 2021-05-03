using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect
{
    [RequireComponent(typeof(PerspectiveSwitcher))]
    public class CameraController : MonoBehaviour
    {
        public static UnityEvent OnFinishTransition = new UnityEvent();
        
        private Transform _transform;
        private PerspectiveSwitcher _perspectiveSwitcher;
        private GameManager _gameManager;

        private void Awake()
        {
            _transform = transform;
            _perspectiveSwitcher = GetComponent<PerspectiveSwitcher>();
            _gameManager = GameManager.Instance;
            GameManager.OnStateChange.AddListener(StateChange);
        }

        private void Start()
        {
            var startState = _gameManager.CurrentState;
            var startPosition = startState.transform;
            _transform.SetPositionAndRotation(startPosition.position, startPosition.rotation);
        }

        private void StateChange(GameState state)
        {
            if ((state.OrthographicView && !_perspectiveSwitcher.IsOrthographic) ||
                (!state.OrthographicView && _perspectiveSwitcher.IsOrthographic))
            {
                _perspectiveSwitcher.Switch(state.CameraTransitionDuration, false);
            }

            var target = state.transform;
            var currentState = _gameManager.CurrentState;

            if ((_transform.position != target.position) || (_transform.rotation != target.rotation))
            {
                _transform.DOMove(target.position, state.CameraTransitionDuration);
                _transform.DORotate(target.rotation.eulerAngles, state.CameraTransitionDuration)
                    .OnStart(() => UpdateUI(currentState.CanvasGroup, false, currentState.UIFadeDuration))
                    .OnComplete(() =>
                    {
                        UpdateUI(state.CanvasGroup, true, state.UIFadeDuration);
                        OnFinishTransition.Invoke();
                    });
            }
            else
            {
                UpdateUI(currentState.CanvasGroup, false, currentState.UIFadeDuration);
                UpdateUI(state.CanvasGroup, true, state.UIFadeDuration);
                OnFinishTransition.Invoke();
            }
        }
        
        private void UpdateUI(CanvasGroup ui, bool show, float duration)
        {
            ui.interactable = ui.blocksRaycasts = show;
            StartCoroutine(FadeUI(ui, ui.alpha, show ? 1 : 0, duration));
        }
        
        private static IEnumerator FadeUI(CanvasGroup ui, float start, float end, float duration)
        {
            var counter = 0f;

            while (counter < duration)
            {
                counter += Time.deltaTime;
                ui.alpha = Mathf.Lerp(start, end, counter / duration);

                yield return null;
            }
        }
    }
}