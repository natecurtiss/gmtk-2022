using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat.Units
{
    abstract class Unit : MonoBehaviour
    {
        [SerializeField] UnityEvent _onStart;
        void Start() => _onStart.Invoke();
        void FixedUpdate() => Go();
        protected abstract void Go();
    }
}