using UnityEngine;

namespace CluckAndCollect
{
    public class MoveCommand : ICommand
    {
        public float Time { get; }
        
        private readonly Vector3 _direction;

        public MoveCommand(Vector3 direction, float time)
        {
            _direction = direction;
            Time = time;
        }

        public void Execute()
        {
        }
    }
}