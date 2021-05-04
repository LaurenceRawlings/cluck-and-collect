using System.Collections.Generic;
using CluckAndCollect.Game.Commands;

namespace CluckAndCollect.Game
{
    public struct ReplayData
    {
        public float StartTime { get; }
        public List<ICommand> Commands { get; }

        public ReplayData(float time)
        {
            Commands = new List<ICommand>();
            StartTime = time;
        }
    }
}