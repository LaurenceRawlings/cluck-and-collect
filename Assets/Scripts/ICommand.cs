using UnityEngine;

namespace CluckAndCollect
{
    public interface ICommand
    {
        public float Time { get; }
        void Execute();
    }
}