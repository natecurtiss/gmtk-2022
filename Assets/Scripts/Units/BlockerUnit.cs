using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat.Units
{
    class BlockerUnit : Unit
    {
        [SerializeField] UnityEvent _onExplode;
        public bool IsBlocking { get; set; }
        
        protected override void Go()
        {
            
        }

        public void Damage(float timeLeft)
        {
            
        }

        public void Explode() => _onExplode.Invoke();
    }
}
