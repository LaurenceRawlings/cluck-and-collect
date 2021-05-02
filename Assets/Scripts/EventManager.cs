using System;
using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect
{
    public class EventManager : MonoBehaviour
    {
        [SerializeField] public UnityEvent onNewGame;
        [SerializeField] public UnityEvent<GameState> onStartSwitchState;
        [SerializeField] public UnityEvent onFinishSwitchState;
    }
}