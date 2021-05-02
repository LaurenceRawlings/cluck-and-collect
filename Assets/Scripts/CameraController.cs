using System;
using DG.Tweening;
using UnityEngine;

namespace CluckAndCollect
{
    [RequireComponent(typeof(PerspectiveSwitcher))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float transitionEase;

        private Transform _transform;
        private PerspectiveSwitcher _perspectiveSwitcher;

        private void Awake()
        {
            _transform = transform;
            _perspectiveSwitcher = GetComponent<PerspectiveSwitcher>();
        }

        private void Start()
        {
            GameManager.Instance.EventManager.onStartSwitchState.AddListener(StateSwitch);
            var start = GameManager.Instance.CurrentState.transform;
            _transform.SetPositionAndRotation(start.position, start.rotation);
        }

        private void StateSwitch(GameState state)
        {
            var target = state.transform;
            var duration = state.CameraTransitionDuration;

            if ((state.OrthographicView && !_perspectiveSwitcher.IsOrthographic) ||
                (!state.OrthographicView && _perspectiveSwitcher.IsOrthographic))
            {
                _perspectiveSwitcher.Switch(duration, transitionEase, false);
            }

            _transform.DOMove(target.position, duration);
            _transform.DORotate(target.rotation.eulerAngles, duration)
                .OnComplete(GameManager.Instance.EventManager.onFinishSwitchState.Invoke);
        }
    }
}