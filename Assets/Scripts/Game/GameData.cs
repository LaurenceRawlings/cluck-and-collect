using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace CluckAndCollect
{
    public struct GameData
    {
        public int Seed { get; }
        public int Score { get; set; }
        public List<MoveCommand> Commands { get; }

        public GameData(int seed = 0)
        {
            Seed = seed == 0 ? DateTime.Now.Millisecond : seed;
            Score = 0;
            Commands = new List<MoveCommand>();
            
            Random.InitState(Seed);
        }
    }
}