using UnityEngine;

namespace CluckAndCollect
{
    [RequireComponent(typeof(MenuController))]
    [RequireComponent(typeof(ChickenController))]
    public class GameManager : MonoBehaviour
    {
        private MenuController _menuController;

        private void Awake()
        {
            _menuController = GetComponent<MenuController>();
        }

        private void OnEnable()
        {
            _menuController.ONPlayButtonClicked += StartGame;
            _menuController.ONSettingsButtonClicked += Settings;
        }
        
        private void OnDisable()
        {
            _menuController.ONPlayButtonClicked -= StartGame;
            _menuController.ONSettingsButtonClicked -= Settings;
        }

        private void StartGame()
        {
            
        }
        
        private void Settings()
        {

        }
    }
}