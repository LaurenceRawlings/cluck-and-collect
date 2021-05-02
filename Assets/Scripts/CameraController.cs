using System;
using DG.Tweening;
using UnityEngine;

namespace CluckAndCollect
{
    [RequireComponent(typeof(PerspectiveSwitcher))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float transitionDuration;
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
        }

        private void StateSwitch(GameState state)
        {
            var target = state.transform;

            if ((state.OrthographicView && !_perspectiveSwitcher.IsOrthographic) ||
                (!state.OrthographicView && _perspectiveSwitcher.IsOrthographic))
            {
                _perspectiveSwitcher.Switch(transitionDuration, transitionEase, false);
            }

            _transform.DOMove(target.position, transitionDuration);
            _transform.DORotate(target.rotation.eulerAngles, transitionDuration)
                .OnComplete(GameManager.Instance.EventManager.onFinishSwitchState.Invoke);
        }
    }
}