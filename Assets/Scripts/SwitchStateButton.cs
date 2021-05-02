using UnityEngine;
using UnityEngine.EventSystems;

namespace CluckAndCollect
{
    public class SwitchStateButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameState state;


        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager.Instance.EventManager.onStartSwitchState.Invoke(state);
        }
    }
}