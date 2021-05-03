namespace CluckAndCollect.Game.Commands
{
    public interface ICommand
    {
        public float Time { get; }
        void Execute();
    }
}