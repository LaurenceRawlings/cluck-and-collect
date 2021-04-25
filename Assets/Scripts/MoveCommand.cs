using UnityEngine;

namespace CluckAndCollect
{
    public class MoveCommand: Command
    {
        private readonly Vector3 _direction;

        public MoveCommand(Vector3 direction)
        {
            _direction = direction;
        }

        public override void Execute()
        {
            ChickenController.Instance.ActiveChicken.GetComponent<Chicken>().Move(_direction);
        }

        public override void Undo()
        {
            ChickenController.Instance.ActiveChicken.GetComponent<Chicken>().Move(_direction * -1);
        }
    }
}