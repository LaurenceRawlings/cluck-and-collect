using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect
{
    public class EventManager : MonoBehaviour
    {
        [SerializeField] public UnityEvent<GameState> onStartSwitchState;
        [SerializeField] public UnityEvent onFinishSwitchState;
        [SerializeField] public UnityEvent onStartGame;
        [SerializeField] public UnityEvent onEndGame;
        [SerializeField] public UnityEvent<Vector3> onMoveCommand;
    }
}