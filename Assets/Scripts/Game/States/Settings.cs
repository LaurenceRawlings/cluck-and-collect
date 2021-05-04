using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CluckAndCollect.Game.States
{
    public class Settings : GameState
    {
        public static readonly UnityEvent OnEnter = new UnityEvent();
        public static readonly UnityEvent OnExit = new UnityEvent();
        public static readonly UnityEvent OnSettingsChange = new UnityEvent();

        [SerializeField] private Slider ambianceSlider;
        [SerializeField] private Slider effectsSlider;

        public override void Enter()
        {
            OnEnter.Invoke();
            ambianceSlider.value = PlayerPrefs.GetFloat("ambiance");
            effectsSlider.value = PlayerPrefs.GetFloat("effects");
        }

        public override void Exit()
        {
            OnExit.Invoke();
        }
        
        public void UpdateAmbianceVolume()
        {
            PlayerPrefs.SetFloat("ambiance", ambianceSlider.value);
            PlayerPrefs.Save();
            OnSettingsChange.Invoke();
        }
        
        public void UpdateEffectsVolume()
        {
            PlayerPrefs.SetFloat("effects", effectsSlider.value);
            PlayerPrefs.Save();
            OnSettingsChange.Invoke();
        }
    }
}