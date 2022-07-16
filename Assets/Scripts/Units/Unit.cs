using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat.Units
{
    abstract class Unit : MonoBehaviour
    {
        [SerializeField] UnityEvent _onStart;
        [field: SerializeField] public UnitType Type { get; private set; } = UnitType.FirstRow;
        
        void Start() => _onStart.Invoke();
        void FixedUpdate() => Go();
        
        protected abstract void Go();
    }
}