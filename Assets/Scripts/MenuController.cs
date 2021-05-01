using System;
using DG.Tweening;
using UnityEngine;

namespace CluckAndCollect
{
    public class MenuController : MonoBehaviour
    {
        public Action ONPlayButtonClicked;
        public Action ONSettingsButtonClicked;
        
        [SerializeField] private new Camera camera;
        [SerializeField] private Transform menuCameraPosition;
        [SerializeField] private Transform playCameraPosition;
        [SerializeField] private Transform settingsCameraPosition;
        [SerializeField] private Transform profilesCameraPosition;

        private Transform _cameraTransform;
        private PerspectiveSwitcher _perspectiveSwitcher;

        private void Awake()
        {
            _cameraTransform = camera.transform;
            _perspectiveSwitcher = camera.GetComponent<PerspectiveSwitcher>();
        }

        private void Start()
        {
            Menu();
        }

        private void OnEnable()
        {
            ONPlayButtonClicked += Play;
            ONSettingsButtonClicked += Settings;
        }
        
        private void OnDisable()
        {
            ONPlayButtonClicked -= Play;
            ONSettingsButtonClicked -= Settings;
        }

        public void PlayButtonClicked()
        {
            if (ONPlayButtonClicked != null)
            {
                ONPlayButtonClicked();
            }
        }
        
        public void SettingsButtonClicked()
        {
            if (ONSettingsButtonClicked != null)
            {
                ONSettingsButtonClicked();
            }
        }

        private void Menu()
        {
            _cameraTransform.DOMove(menuCameraPosition.position, 3f);
            _cameraTransform.DORotate(menuCameraPosition.rotation.eulerAngles, 3f);
        }

        private void Play()
        {
            _perspectiveSwitcher.Switch(3f, 0.05f, false);
            _cameraTransform.DOMove(playCameraPosition.position, 3f);
            _cameraTransform.DORotate(playCameraPosition.rotation.eulerAngles, 3f);
        }
        
        private void Settings()
        {
            _cameraTransform.DOMove(settingsCameraPosition.position, 3f);
            _cameraTransform.DORotate(settingsCameraPosition.rotation.eulerAngles, 3f);
        }
    }
}