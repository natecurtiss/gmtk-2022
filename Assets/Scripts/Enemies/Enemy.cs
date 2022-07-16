using PieceCombat.Units;
using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat.Enemies
{
    abstract class Enemy : MonoBehaviour
    {
        [SerializeField] UnityEvent _onExplode;
        [SerializeField] int _damage = 1;
        protected bool CanMove { get; private set; }

        void OnTriggerEnter(Collider col)
        {
            if (col.TryGetComponent<Unit>(out var unit))
            {
                if (unit is BlockerUnit)
                {
                    // TODO: Wait for destroy.
                    CanMove = false;
                }
                else
                {
                    Explode();
                }
            }
            else if (col.TryGetComponent<HomeBase>(out var homeBase))
            {
                homeBase.Damage(_damage);
                Explode();
            }
        }

        void Explode()
        {
            _onExplode.Invoke();
            Destroy(gameObject);
        }
    }
}
