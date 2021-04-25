﻿using System;

namespace CluckAndCollect
{
    public abstract class Command
    {
        public abstract void Execute();

        public abstract void Undo();
    }
}