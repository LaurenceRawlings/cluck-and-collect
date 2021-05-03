using UnityEngine;

namespace CluckAndCollect.Behaviours
{
    public class River : MonoBehaviour
    {
        [SerializeField] private ParticleSystem splash;

        private void OnTriggerEnter(Collider other)
        {
            Instantiate(splash, other.transform.position, Quaternion.identity);
        }
    }
}