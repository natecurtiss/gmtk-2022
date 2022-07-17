using System;
using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat
{
    class ExplodeAtTheEnd : MonoBehaviour
    {
        public static event Action OnHitHomeBase;
        [SerializeField] UnityEvent _onExplode;
        [SerializeField] float _end = 20f;
        [SerializeField] Compare _compare = Compare.GreaterThan;

        void Update()
        {
            if (_compare == Compare.GreaterThan)
            {
                if (transform.position.x >= _end)
                {
                    _onExplode.Invoke();
                    Destroy(gameObject);
                }
            }
            else
            {
                if (transform.position.x <= _end)
                {
                    OnHitHomeBase?.Invoke();
                    _onExplode.Invoke();
                    Destroy(gameObject);
                }
            }
        }
    }
}