using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace CluckAndCollect
{
    public class ChickenController : MonoBehaviour
    {
        public static ChickenController Instance { get; private set; }

        [FormerlySerializedAs("_queuePositions")] [SerializeField]
        private Transform[] queuePositions;

        [SerializeField]
        private Transform queueExitPosition;
        
        [SerializeField]
        private Transform queueEntryPosition;

        [FormerlySerializedAs("_chickenPrefab")] [SerializeField]
        private GameObject chickenPrefab;
        
        public GameObject ActiveChicken { get; private set; }
        private Queue<GameObject> _chickenQueue = new Queue<GameObject>();
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;

                foreach (var queuePosition in queuePositions)
                {
                    _chickenQueue.Enqueue(Instantiate(chickenPrefab, queuePosition.position, queuePosition.rotation));
                }
                
                NewChicken();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                NewChicken();
            }
        }

        public void NewChicken()
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

            ActiveChicken = nextChicken;
        }
    }
}