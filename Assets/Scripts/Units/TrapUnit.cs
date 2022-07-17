using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat.Units
{
    class TrapUnit : Unit
    {
        [SerializeField] UnityEvent _onExplode;
        [SerializeField] ParticleSystem _particles;

        protected override void Go()
        {
            
        }

        public void Explode()
        {
            var p = _particles.main;
            p.loop = false;
            _onExplode.Invoke();
        }
    }
}