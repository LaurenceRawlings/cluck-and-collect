using UnityEngine;

namespace CluckAndCollect
{
    public class MoveCommand : ICommand
    {
        public float Time { get; }

        private readonly GameManager _gameManager;
        private readonly Vector3 _direction;

        public MoveCommand(Vector3 direction, float time)
        {
            _gameManager = GameManager.Instance;
            _direction = direction;
            Time = time;
        }

        public void Execute()
        {
            _gameManager.EventManager.onMoveCommand.Invoke(_direction);
        }
    }
}