using System;

namespace CluckAndCollect.Game.Commands
{
    public interface ICommand : IComparable<ICommand>
    {
        public float Time { get; }
        void Execute();
    }
}