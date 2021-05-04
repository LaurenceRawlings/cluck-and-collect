using System;
using CluckAndCollect.Game;
using CluckAndCollect.Game.States;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CluckAndCollect.Behaviours
{
    public class Coop : MonoBehaviour
    {
        public static readonly UnityEvent OnCollect = new UnityEvent();

        [SerializeField] private Transform sitPosition;
        [SerializeField] private SpriteRenderer occupied;
        
        private BoxCollider _grid;
        private GameObject _chicken;

        private void Awake()
        {
            _grid = GetComponentInChildren<BoxCollider>();
        }

        private void Start()
        {
            Play.OnCoopsFilled.AddListener(Clear);
            Play.OnEnter.AddListener(Clear);
            occupied.enabled = false;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            
            _grid.gameObject.layer = (int) Mathf.Log(GameManager.Instance.WallLayer.value, 2);
            _chicken = other.gameObject;
            _chicken.transform.DOMove(sitPosition.position, 0.1f).SetDelay(1f);
            _chicken.transform.DORotate(sitPosition.rotation.eulerAngles, 0.1f).SetDelay(1f);
            occupied.enabled = true;
            OnCollect.Invoke();
        }

        private void Clear()
        {
            if (_chicken)
            {
                _chicken.transform.DOScale(0, 1f).OnComplete(() => Destroy(_chicken));
                _chicken = null;
            }
            
            _grid.gameObject.layer = (int) Mathf.Log(GameManager.Instance.GridLayer.value, 2);
            occupied.enabled = false;
        }
    }
}