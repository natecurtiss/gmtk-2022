using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat.Units
{
    class TrapUnit : Unit
    {
        [SerializeField] UnityEvent _onExplode;   
        
        protected override void Go()
        {
            
        }

        public void Explode()
        {
            _onExplode.Invoke();
            Destroy(gameObject);
        }
    }
}