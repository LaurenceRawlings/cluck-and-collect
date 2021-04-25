using System;
using UnityEngine;

namespace CluckAndCollect
{
    public interface IEntity
    {
        Transform transform { get; }
        void Move(Vector3 direction);
    }
}