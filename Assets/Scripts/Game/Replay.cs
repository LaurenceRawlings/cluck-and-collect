using System;
using System.Collections.Generic;

namespace CluckAndCollect
{
    public class Replay
    {
        private Dictionary<float, Command> _sequence = new Dictionary<float, Command>();

        public Command GetCommand(float time)
        {
            return _sequence.TryGetValue(time, out var command) ? command : null;
        }

        public void SetCommand(float time, Command command)
        {
            _sequence.Add(time, command);
        }
    }
}