using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CluckAndCollect
{
    public class ChickenController : MonoBehaviour
    {
        [SerializeField] private Transform[] queuePositions;
        [SerializeField] private Transform queueExitPosition;
        [SerializeField] private Transform queueEntryPosition;
        [SerializeField] private GameObject chickenPrefab;

        private GameObject _activeChicken;
        private Queue<GameObject> _chickenQueue = new Queue<GameObject>();

        private void OnEnable()
        {
            foreach (var queuePosition in queuePositions)
            {
                _chickenQueue.Enqueue(Instantiate(chickenPrefab, queuePosition.position, queuePosition.rotation));
            }

            NewChicken();
        }

        private void NewChicken()
        {
            var sequence = DOTween.Sequence();

            var nextChicken = _chickenQueue.Dequeue();
            sequence.Append(nextChicken.transform.DOJump(queueExitPosition.position, 1, 1, 0.5f));
            sequence.Join(nextChicken.transform.DOLocalRotate(Vector3.zero, 0.5f));
            _chickenQueue.Enqueue(Instantiate(chickenPrefab, queueEntryPosition.position, queueEntryPosition.rotation));

            var chickens = _chickenQueue.ToArray();

            for (var i = 0; i < _chickenQueue.Count; i++)
            {
                sequence.Append(chickens[i].transform.DOJump(queuePositions[i].position, 0.25f, 4, 1));
                sequence.AppendInterval(0.1f);
            }

            _activeChicken = nextChicken;
        }
    }
}