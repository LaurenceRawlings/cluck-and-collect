using System;
using UnityEngine;

namespace CluckAndCollect
{
    [Serializable]
    public struct SpawnItem : IComparable<SpawnItem>
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float Weighting { get; private set; }
        
        public int CompareTo(SpawnItem other)
        {
            return Weighting.CompareTo(other.Weighting);
        }
    }
}