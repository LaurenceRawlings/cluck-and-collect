using System.Collections.Generic;
using System.Linq;
using CluckAndCollect.Game.States;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect.Behaviours
{
    public class ChickenQueue : MonoBehaviour
    {
        public static readonly UnityEvent OnReady = new UnityEvent();

        public GameObject ActiveChicken { get; private set; }

        [SerializeField] private Transform[] queuePositions;
        [SerializeField] private Transform queueExitPosition;
        [SerializeField] private Transform queueEntryPosition;
        [SerializeField] private GameObject chickenPrefab;

        private Queue<GameObject> _chickenQueue = new Queue<GameObject>();
        private List<GameObject> _spawnedChickens = new List<GameObject>();

        private void Start()
        {
            Play.OnTurnFinished.AddListener(NewChicken);
            Play.OnEnter.AddListener(Clear);
            Replay.OnEnter.AddListener(Clear);
        }

        private void Clear()
        {
            foreach (var chicken in _spawnedChickens.Where(chicken => chicken != null))
            {
                Destroy(chicken);
            }
            
            _chickenQueue.Clear();
            
            foreach (var queuePosition in queuePositions)
            {
                var chicken = Instantiate(chickenPrefab, queuePosition.position, queuePosition.rotation);
                _chickenQueue.Enqueue(chicken);
                _spawnedChickens.Add(chicken);
            }

            NewChicken();
        }

        private void NewChicken()
        {
            var sequence = DOTween.Sequence().SetEase(Ease.Linear);

            var nextChicken = _chickenQueue.Dequeue();
            var position = queueExitPosition.position;
            
            sequence.Append(nextChicken.transform.DOJump(position, 0.25f,
                    (int) Mathf.Ceil(Vector3.Distance(nextChicken.transform.position, position)), 0.5f)
                .OnComplete(() => OnReady.Invoke()));
            sequence.Join(nextChicken.transform.DOLocalRotate(Vector3.zero, 0.5f));

            var chicken = Instantiate(chickenPrefab, queueEntryPosition.position, queueEntryPosition.rotation);
            _chickenQueue.Enqueue(chicken);
            _spawnedChickens.Add(chicken);

            var chickens = _chickenQueue.ToArray();

            for (var i = 0; i < _chickenQueue.Count; i++)
            {
                sequence.Append(chickens[i].transform.DOJump(queuePositions[i].position, 0.25f, 4, 1));
                sequence.AppendInterval(0.1f);
            }

            ActiveChicken = nextChicken;
        }
    }
}